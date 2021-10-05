import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Kunde } from "../Kunde";
import { Billett } from "../Billett";
import { Avgang } from "../Avgang";
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router'
import { Lugar } from "../Lugar";

@Component({
  selector: 'visBillett',
  templateUrl: "visBillett.html",
  styleUrls: ["visBillettStyle.css"],
  encapsulation: ViewEncapsulation.None
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
  bestilteLugarer: Array<Lugar>;
  bestilteLugarerRetur: Array<Lugar>;

  avgangBilplass: String;
  returBilplass: String;

  visRetur: boolean = false;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute, private location: Location) {
    
  }


  ngOnInit() {
    this.billettId = this._ActivatedRoute.snapshot.paramMap.get('id');
    this.hentBillett();
    this.hentAvgang();
  }

  hentBillett() {
    this.http.get<Billett>("api/Bestilling/hentBillett/" + this.billettId).
      subscribe(hentetBillett => {
        this.billett = hentetBillett;

        this.avgangsId = this.billett.avgangId;
        this.hentAvgang();

        if (this.billett.avgangIdRetur != null) {
          this.returId = this.billett.avgangIdRetur;
          this.bestilteLugarerRetur = this.billett.lugarerRetur;
          this.hentRetur();
          this.visRetur = true;
        }

        this.kundeId = this.billett.kundeId
        this.hentKunde();

        this.bestilteLugarer = this.billett.lugarer;
 
        this.checkBilplass();
      },
        error => console.log(error)
      );
  }

  hentKunde() {
    this.http.get<Kunde>("api/Bestilling/hentKunde/" + this.kundeId).
      subscribe(kunde => {
        this.kunde = kunde;
      },
        error => console.log(error)
      );
  }

  hentAvgang() {
    this.http.get<Avgang>("api/Bestilling/hentValgtAvgang/" + this.avgangsId).
      subscribe(avgang => {
        this.avgang = avgang;
      },
        error => console.log(error)
    )
  }

  hentRetur() {
    this.http.get<Avgang>("api/Bestilling/hentValgtAvgang/" + this.returId).
      subscribe(avgang => {
        this.retur = avgang;
      },
        error => console.log(error)
    )
  }

  checkBilplass() {
    if (this.billett.bilplass) {
      this.avgangBilplass = "Ja"
    }
    else {
      this.avgangBilplass = "Nei"
    }
    if (this.billett.bilplassRetur) {
      this.returBilplass = "Ja"
    }
    else {
      this.returBilplass = "Nei"
    }

  }
}
