import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Kunde } from "../Kunde";
import { Billett } from "../Billett";
import { Avgang } from "../Avgang";
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'visBillett',
  templateUrl: "visBillett.html"
})

export class visBillett {
  kunde: Kunde;
  kundeId: number;
  billett: Billett;
  billettId: String = "";
  avgang: Avgang;
  avgangsId: number;
  retur: Avgang;
  returId: number;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute, private location: Location) {
    
  }

  ngOnInit() {
    this.billettId = this._ActivatedRoute.snapshot.paramMap.get('id');
    this.hentBillett();
    this.hentAvgang();
    //if (this.billett.returId) {
    //  this.hentRetur();
    //}
  }

  hentBillett() {
    this.http.get<Billett>("api/Bestilling/hentBillett/" + this.billettId).
      subscribe(hentetBillett => {
        this.billett = hentetBillett;
        console.log("BIIIILLLLLL!!!!1");
        console.log(this.billett);

        this.avgangsId = this.billett.avgangId;
        this.hentAvgang();

        if (this.billett.avgangIdRetur) {
          this.returId = this.billett.avgangIdRetur;
          this.hentRetur();
        }

        this.kundeId = this.billett.kundeId
        this.hentKunde();
      },
        error => console.log(error)
      );
  }

  hentKunde() {
    this.http.get<Kunde>("api/Bestilling/hentKunde/" + this.kundeId).
      subscribe(kunde => {
        this.kunde = kunde;
        console.log("KUNDE:")
        console.log(this.kunde);
      },
        error => console.log(error)
      );
  }

  hentAvgang() {
    this.http.get<Avgang>("api/Bestilling/hentAvgang" + this.avgangsId).
      subscribe(avgang => {
        this.avgang = avgang;
      },
        error => console.log(error)
    )
  }

  hentRetur() {
    this.http.get<Avgang>("api/Bestilling/hentAvgang" + this.returId).
      subscribe(avgang => {
        this.retur = avgang;
      },
        error => console.log(error)
    )
  }
}
