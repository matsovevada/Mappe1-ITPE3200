import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Avgang } from '../Avgang';
import { Strekning } from '../Strekning';

@Component({
  selector: 'bestilling',
  templateUrl: 'bestilling.html'
})
export class Bestilling {
  alleAvganger: Array<Avgang>;
  alleStrekninger: Array<Strekning>;
  strekning: String;
  avgangValgtDatoTidTicks: number;
  avgangID: String;
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

        this.avgangID = avgangene[0].id.toString();
        this.avgangValgtDatoTidTicks = avgangene[0].datoTidTicks;

        console.log("avgangChange")
        console.log(this.avgangID);
        console.log(this.avgangValgtDatoTidTicks);
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

  toggleValgtStrekning() {
    this.hentAlleAvganger();
    this.settStrekningRetur();
    this.hentAlleAvgangerRetur();
    this.strekningValgt = true;
  }

  toggleValgtAvgang() {
    console.log("TRYKKET!");
    this.valgtAvgang = true;

    let avgangValgt = JSON.parse((<HTMLSelectElement>document.getElementById('avgang')).value);
    this.avgangID = avgangValgt['id'];
    this.avgangValgtDatoTidTicks = avgangValgt['datoTidTicks'];

    console.log("avgangChange")
    console.log(this.avgangID);
    console.log(this.avgangValgtDatoTidTicks);
  }


  //RETUR

  hentAlleAvgangerRetur() {
    this.http.get<Avgang[]>("api/Bestilling/hentAvgangRetur/" + this.strekningRetur)
      .subscribe(avgangene => {
        this.alleAvgangerRetur = avgangene;
        this.laster = false;

        this.avgangIdRetur = avgangene[0].id.toString();
        this.avgangValgtReturDatoTidTicks = avgangene[0].datoTidTicks;

        console.log("avgangChangeRetur")
        console.log(this.avgangIdRetur);
        console.log(this.avgangValgtReturDatoTidTicks);
      },
        error => console.log(error)
      );
  }

  settStrekningRetur() {
    let returStrekningSplit = this.strekning.split(" - ");
    this.strekningRetur = returStrekningSplit[1] + " - " + returStrekningSplit[0];
  }

  toggleValgtRetur() {
    console.log("TRYKKET CHECKBOX!!!");
    this.valgtRetur = !this.valgtRetur;
  }


  toggleValgtAvgangRetur() {
    console.log("TRYKKET!");
    this.valgtAvgangRetur = true;

    let avgangValgt = JSON.parse((<HTMLSelectElement>document.getElementById('avgangRetur')).value);
    this.avgangIdRetur = avgangValgt['id'];
    this.avgangValgtReturDatoTidTicks = avgangValgt['datoTidTicks'];

    console.log("avgangChangerRetur")
    console.log(this.avgangIdRetur);
    console.log(this.avgangValgtReturDatoTidTicks);
  }
}
