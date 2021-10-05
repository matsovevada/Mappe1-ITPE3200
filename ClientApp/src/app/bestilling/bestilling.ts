import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Avgang } from '../Avgang';
import { Strekning } from '../Strekning';
import { ViewEncapsulation } from "@angular/core";

@Component({
  selector: 'bestilling',
  templateUrl: 'bestilling.html',
  styleUrls: [
    'bestillingStylesheet.css'
  ],
  encapsulation: ViewEncapsulation.None,
})

export class Bestilling {
  alleAvganger: Array<Avgang>;
  alleStrekninger: Array<Strekning>;
  strekning: String;
  avgangValgtDatoTidTicks: number;
  laster: boolean;
  strekningValgt: boolean = false;
  valgtAvgang: boolean = false;
  avgangId: String;
  gyldigAvgang: boolean = false;

  alleAvgangerRetur: Array<Avgang>;
  strekningRetur: String;
  valgtRetur: boolean = false;
  valgtAvgangRetur: boolean = false;
  avgangIdRetur: String;
  avgangValgtReturDatoTidTicks: number;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleStrekninger();
  }

  hentAlleAvganger() {
    this.http.get<Avgang[]>("api/Bestilling/hentAvgang/" + this.strekning)
      .subscribe(avgangene => {
        this.alleAvganger = avgangene;
        this.laster = false;

        this.avgangId = avgangene[0].id.toString();
        this.avgangValgtDatoTidTicks = avgangene[0].datoTidTicks;
      },
        error => console.log(error)
      );
  }

  hentAlleStrekninger() {
    this.http.get<Strekning[]>("api/Bestilling/hentStrekning")
      .subscribe(strekningene => {
        console.log(strekningene)
        this.alleStrekninger = strekningene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  /*
   * Gjør avganger (dropdown) synlig i front. Setter returstrekning og henter avganger for retur.
   */

  toggleValgtStrekning() {
    this.hentAlleAvganger();
    this.settStrekningRetur();
    this.hentAlleAvgangerRetur();
    this.strekningValgt = true;
  }

  /* DatotidTicks brukes for å sammenligne tidspunkt for avganger slik at bruker ikke kan velge en retur som er satt opp til å gå før tur (avgang) */
  toggleValgtAvgang() {
    this.valgtAvgang = true;
    let avgangValgt = JSON.parse((<HTMLSelectElement>document.getElementById('avgang')).value);
    this.avgangId = avgangValgt['id'];
    this.avgangValgtDatoTidTicks = avgangValgt['datoTidTicks'];
  }


  //RETUR

  hentAlleAvgangerRetur() {
    this.http.get<Avgang[]>("api/Bestilling/hentAvgangRetur/" + this.strekningRetur)
      .subscribe(avgangene => {
        this.alleAvgangerRetur = avgangene;
        this.laster = false;

        this.avgangIdRetur = avgangene[0].id.toString();
        this.avgangValgtReturDatoTidTicks = avgangene[0].datoTidTicks;
      },
        error => console.log(error)
      );
  }

  settStrekningRetur() {
    let returStrekningSplit = this.strekning.split(" - ");
    this.strekningRetur = returStrekningSplit[1] + " - " + returStrekningSplit[0];
  }

  toggleValgtRetur() {
    this.valgtRetur = !this.valgtRetur;
  }


  toggleValgtAvgangRetur() {
    this.valgtAvgangRetur = true;

    let avgangValgt = JSON.parse((<HTMLSelectElement>document.getElementById('avgangRetur')).value);
    this.avgangIdRetur = avgangValgt['id'];
    this.avgangValgtReturDatoTidTicks = avgangValgt['datoTidTicks'];
  }
}
