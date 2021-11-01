import { Component, ViewEncapsulation } from "@angular/core";
import { ActivatedRoute } from '@angular/router'
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Baat } from '../../Baat';
import { Lugar } from '../../Lugar';
import { Strekning } from '../../Strekning';
import { Avgang } from '../../Avgang';

@Component({
  selector: 'endreAvgang',
  templateUrl: 'endreAvgang.html',
  styleUrls: ['StyleSheet.css'],
  encapsulation: ViewEncapsulation.None
})

export class EndreAvgang {
  laster: boolean = false;
  alleBaater: Array<Baat>
  alleStrekninger: Array<Strekning>
  alleLugarer: Array<Lugar>
  datoDag: number;
  datoManed: number;
  datoAr: number;
  datoTime: number;
  datoMinutt: number;
  aktiv: boolean = true;
  bilplasser: number;
  avgang: Avgang;
  avgangId: number;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.avgangId = Number(this._ActivatedRoute.snapshot.paramMap.get('avgangId'));
    this.laster = true;
    this.hentAlleBaater();
    this.hentAlleStrekninger();
    this.hentAlleLugarer();
    this.hentAvgang(this.avgangId);
  }

  hentAvgang(avgangId) {
    this.http.get<Avgang>("api/Bestilling/hentValgtAvgang/" + avgangId)
      .subscribe(avgang => {
        this.avgang = avgang;

        console.log(avgang.datoTid);

        this.datoDag = Number(avgang.datoTid.split("/")[0]);
        this.datoManed = Number(avgang.datoTid.split("/")[1]);
        this.datoAr = Number(avgang.datoTid.split("/")[2].split(" ")[0]);

        this.datoTime = Number(avgang.datoTid.split(" ")[1].split(":")[0]);
        this.datoMinutt = Number(avgang.datoTid.split(" ")[1].split(":")[1]);

        this.bilplasser = Number(avgang.antallLedigeBilplasser);
        this.aktiv = avgang.aktiv;
      },
        error => console.log(error)
      );
  }

  hentAlleBaater() {
    this.http.get<Baat[]>("api/Bestilling/hentBaater")
      .subscribe(baatene => {
        this.alleBaater = baatene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  hentAlleStrekninger() {
    this.http.get<Strekning[]>("api/Bestilling/hentStrekning")
      .subscribe(strekningene => {
        this.alleStrekninger = strekningene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  hentAlleLugarer() {
    this.http.get<Lugar[]>("api/Bestilling/hentAlleLugarer")
      .subscribe(lugarene => {
        console.log("LUG:")
        console.log(lugarene)
        this.alleLugarer = lugarene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  submit() {

    let selectBaat = (document.getElementById('selectBaat')) as HTMLSelectElement;
    let baatNavn = selectBaat.value;

    let selectStrekning = (document.getElementById('selectStrekning')) as HTMLSelectElement;
    let strekningFra = selectStrekning.value.split(" - ")[0];
    let strekningTil = selectStrekning.value.split(" - ")[1];

    console.log(this.datoDag);
    console.log(this.datoManed);
    console.log(this.datoAr);
    console.log(this.datoTime);
    console.log(this.datoMinutt);

    let selectLugarer = (document.getElementById('selectLugarer')) as HTMLSelectElement;
    let options = selectLugarer.selectedOptions;
    var values = Array.from(options).map(({ value }) => value);

    let lugarer = ""
    lugarer += values[0];

    for (let i = 1; i < values.length; i++) {
      lugarer += "," + values[i];
    }

    this.http.put("api/Bestilling/endreAvgang/" + this.avgangId.toString() + "/" + baatNavn + "/" + strekningFra + "/" + strekningTil + "/" + this.datoDag.toString() + "/" + this.datoManed.toString() + "/" + this.datoAr.toString() + "/" + this.datoTime.toString() + "/" + this.datoMinutt + "/" + this.bilplasser + "/" + lugarer + "/" + this.aktiv.toString(), null)
      .subscribe(ok => {
        console.log(ok);
      },
        error => console.log(error)
      );
  }
}
