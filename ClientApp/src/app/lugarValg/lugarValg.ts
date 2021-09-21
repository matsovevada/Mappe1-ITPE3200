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
  avgangsID: string;
  alleLugarer: Array<Lugar>
  laster: boolean;
  valgtAvgang: Avgang;


  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.avgangsID = this._ActivatedRoute.snapshot.paramMap.get('id');
    console.log(this.avgangsID)
    this.laster = true;
    this.hentValgtAvgang(this.avgangsID);
    this.alleLugarer = this.valgtAvgang.ledigeLugarer
    this.hentAlleLugarer();
  }
  
  hentValgtAvgang(ID) {
    this.http.get<Avgang>("api/Bestilling/hentValgtAvgang/" + ID)
      .subscribe(avgang => {
        console.log(avgang)
        this.valgtAvgang = avgang;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  hentAlleLugarer() {
    console.log(this.valgtAvgang);
    console.log(this.alleLugarer);
  }
}
