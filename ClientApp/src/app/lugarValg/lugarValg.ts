import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Lugar } from '../Lugar'
import { Strekning } from '../Strekning'
import { Avgang } from '../Avgang'
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'lugar-valg',
  templateUrl: 'lugarValg.html'
})
export class LugarValg {
  avgangsID: String;
  alleLugarer: Array<Lugar>
  laster: boolean;
  valgtAvgang: Avgang;


  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.avgangsID = this._ActivatedRoute.snapshot.paramMap.get('id');
    console.log(this.avgangsID)
    this.laster = true;
    this.hentValgtAvgang();
  }
  
  hentValgtAvgang() {
    console.log(this.avgangsID);
    this.http.get<Avgang>("api/Bestilling/hentValgtAvgang/" + this.avgangsID)
      .subscribe(avgang => {
        console.log(avgang)
        this.valgtAvgang = avgang;
        this.alleLugarer = this.valgtAvgang.ledigeLugarer;
        this.laster = false;

        this.hentAlleLugarer();
      },
        error => console.log(error)
      );
  }

  hentAlleLugarer() {
    console.log(this.valgtAvgang);
    console.log(this.alleLugarer);
  }
}
