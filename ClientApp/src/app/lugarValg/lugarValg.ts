import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Lugar } from '../Lugar'
import { Strekning } from '../Strekning'
import { Avgang } from '../Avgang'
import { ActivatedRoute, NavigationExtras } from '@angular/router'
import { Billett } from '../Billett';

@Component({
  selector: 'lugar-valg',
  templateUrl: 'lugarValg.html',
  styleUrls: ['StyleSheet.css'],
  encapsulation: ViewEncapsulation.None,
})

export class LugarValg implements OnInit {
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
  lugarerTotalPris: number = 0;
  BILPLASS_PRIS: number = 500;
  returValgt: boolean = false;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit() {
    /* Tar inn avgangsID(er)*/
    this.avgangsID = this._ActivatedRoute.snapshot.paramMap.get('avgang1');
    this.avgangsIDRetur = this._ActivatedRoute.snapshot.paramMap.get('avgang2');

    if (this._ActivatedRoute.snapshot.paramMap.get('valgtRetur') == 'false') {
      this.avgangsIDRetur = "undefined";
    }

    this.laster = true;
    this.hentValgtAvgang();
    this.valgteLugarer = [];
    this.billett = new Billett(); 
    this.billett.avgangId = Number(this.avgangsID);
    this.sjekkOmReturSkalBestilles();
    this.billett.antallPersoner = 1;
  }

  /* Henter avgangen for å vise lugarer på bestillingssider */
  hentValgtAvgang() {
    this.http.get<Avgang>("api/Bestilling/hentValgtAvgang/" + this.avgangsID)
      .subscribe(avgang => {
        this.valgtAvgang = avgang;
        this.alleLugarer = this.valgtAvgang.ledigeLugarer;
        this.laster = false;

      },
        error => console.log(error)
      );
  }
  
  leggTilLugar(lugar: Lugar) {
    this.valgteLugarer.push(lugar);
    this.billett.lugarer = this.valgteLugarer;
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


  changeBilplass() {
    this.billett.bilplass = this.valgtBilplass;

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

  /* Hvis retur er valgt skal bruker redirctes til lugerValg for retur. Verdien som avgjør dette settes her  */
  sjekkOmReturSkalBestilles() {
    if (this.avgangsIDRetur != "undefined") {
      this.billett.avgangIdRetur = Number(this.avgangsIDRetur);
      this.returValgt = true
    }; 
  }

  settAntallPersoner() {
    this.billett.antallPersoner = this.personer;
  }

}
