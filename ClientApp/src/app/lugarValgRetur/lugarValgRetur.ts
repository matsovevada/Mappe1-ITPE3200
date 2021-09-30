import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { Lugar } from '../Lugar'
import { Strekning } from '../Strekning'
import { Avgang } from '../Avgang'
import { ActivatedRoute, NavigationExtras } from '@angular/router'
import { Billett } from '../Billett';

@Component({
  selector: 'lugar-valg-retur',
  templateUrl: 'lugarValgRetur.html'
})
export class LugarValgRetur implements OnInit {
  avgangsID: String;
  avgangsIDRetur: String;
  alleLugarer: Array<Lugar>
  laster: boolean;
  valgtAvgang: Avgang;
  valgteLugarer: Array<Lugar>
  personer: number = 1;
  billett: Billett;
  nokSengeplasser: boolean = false;
  valgtBilplass: boolean = false;
  lugarerTotalPris: number;
  BILPLASS_PRIS: number = 500;

  constructor(private http: HttpClient, private router: Router, private location: Location) {
    this.billett = this.router.getCurrentNavigation().extras.state.billett;
    this.lugarerTotalPris = this.billett.totalPris;
  }

  ngOnInit() {
    this.laster = true;
    console.log("BILLLETTT!");
    console.log(this.billett);
    this.hentValgtAvgang();
    this.valgteLugarer = [];

  }

  hentValgtAvgang() {
    console.log(this.billett.avgangIdRetur);
    this.http.get<Avgang>("api/Bestilling/hentValgtAvgang/" + this.billett.avgangIdRetur)
      .subscribe(avgang => {
        console.log(avgang)
        this.valgtAvgang = avgang;
        this.alleLugarer = this.valgtAvgang.ledigeLugarer;
        this.laster = false;

      },
        error => console.log(error)
      );
  }
  
  leggTilLugar(lugar: Lugar) {
    this.valgteLugarer.push(lugar);
    this.billett.lugarerRetur = this.valgteLugarer;
    this.nokSengeplasser = this.sjekkPersonerSengeplasser();
    this.lugarerTotalPris += lugar.pris;
    this.billett.totalPris = this.lugarerTotalPris;
    lugar.antallLedige--;
  }

  fjernLugar(lugar: Lugar) {
    
    let lugarIndex = -1;

    for (let i = 0; i < this.valgteLugarer.length; i++) {

      if (this.valgteLugarer[i].navn == lugar.navn) {
        lugarIndex = i;
        this.lugarerTotalPris -= this.valgteLugarer[i].pris;
        this.billett.totalPris = this.lugarerTotalPris;
        lugar.antallLedige++;
        break;
      }
    }

    if (lugarIndex >= 0) {
      this.valgteLugarer.splice(lugarIndex, 1);
      this.billett.lugarerRetur = this.valgteLugarer;
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

  changeBilplass() {
    this.billett.bilplassRetur = this.valgtBilplass;

    if (this.valgtBilplass) {
      this.lugarerTotalPris += this.BILPLASS_PRIS;
      this.billett.totalPris = this.lugarerTotalPris;
      this.valgtAvgang.antallLedigeBilplasser--;
    }
    else {
      this.lugarerTotalPris -= this.BILPLASS_PRIS;
      this.billett.totalPris = this.lugarerTotalPris;
      this.valgtAvgang.antallLedigeBilplasser++;
    }
  }

  settAntallPersoner() {
    this.billett.antallPersonerRetur = this.personer;
  }


}
