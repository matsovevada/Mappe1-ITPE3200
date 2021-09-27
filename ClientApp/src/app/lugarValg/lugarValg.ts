import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Lugar } from '../Lugar'
import { Strekning } from '../Strekning'
import { Avgang } from '../Avgang'
import { ActivatedRoute } from '@angular/router'
import { Billett } from '../Billett';

@Component({
  selector: 'lugar-valg',
  templateUrl: 'lugarValg.html'
})
export class LugarValg implements OnInit {
  avgangsID: String;
  alleLugarer: Array<Lugar>
  laster: boolean;
  valgtAvgang: Avgang;
  valgteLugarer: Array<Lugar>
  personer: number = 1;
  billett: Billett;
  nokSengeplasser: boolean = false;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.avgangsID = this._ActivatedRoute.snapshot.paramMap.get('id');
    console.log(this.avgangsID)
    this.laster = true;
    this.hentValgtAvgang();
    this.valgteLugarer = [];
    this.billett = new Billett();
    this.billett.avgangId = Number(this.avgangsID);
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
    this.valgteLugarer.push(lugar);
    this.billett.lugarer = this.valgteLugarer;
    this.nokSengeplasser = this.sjekkPersonerSengeplasser();
  }

  fjernLugar(lugarNavn: string) {
    
    let lugarIndex = -1;

    for (let i = 0; i < this.valgteLugarer.length; i++) {

      if (this.valgteLugarer[i].navn == lugarNavn) {
        lugarIndex = i;
        break;
      }
    }

    if (lugarIndex >= 0) {
      this.valgteLugarer.splice(lugarIndex, 1);
      this.billett.lugarer = this.valgteLugarer;
    }

    this.nokSengeplasser = this.sjekkPersonerSengeplasser();
  }

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
    else return true;
  }

  gaaTilKvittering() {
    alert(this.sjekkPersonerSengeplasser());
  }

  hentAlleLugarer() {
    console.log(this.valgtAvgang);
    console.log(this.alleLugarer);
  }
}
