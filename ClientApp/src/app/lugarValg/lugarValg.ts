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
  valgteLugarer: Array<Lugar>
  personer: number;

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

  leggTilLugar(lugar: Lugar) {
    console.log("onAddLugar");
    console.log(lugar);
    this.valgteLugarer.push(lugar);
  }

  /*fjernTilLugar(lugar: Lugar) {
    this.valgteLugarer.splice(lugar)
  }*/

  // sjekk at kunden har valgt mange nok sengeplasser i forhold til hvor mange personer som skal v?re med p? turen
  // returner true hvis hvis kunden har valgt nok sengeplasser
  // returnerer false hvis kunden ikke har valgt nok sengeplasser
  sjekkPersonerSengeplasser() {

    var sengeplasserTotal = 0;

    this.valgteLugarer.forEach(lugar => {
      sengeplasserTotal += lugar.antallSengeplasser;
    });

    if (this.personer > sengeplasserTotal) {
      return false;
    }

    return true;
  }

  hentAlleLugarer() {
    console.log(this.valgtAvgang);
    console.log(this.alleLugarer);
  }
}
