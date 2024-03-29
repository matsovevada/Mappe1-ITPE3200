import { Component, ViewEncapsulation } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Baat } from '../../Baat';
import { Lugar } from '../../Lugar';
import { Strekning } from '../../Strekning';
import { Avgang } from '../../Avgang';

@Component({
  selector: 'addAvgang',
  templateUrl: 'addAvgang.html',
  styleUrls: ['StyleSheet.css'],
  encapsulation: ViewEncapsulation.None
})

export class AddAvgang {
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
 
  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.loggetInnSjekk();
    this.hentAlleBaater();
    this.hentAlleStrekninger();
    this.hentAlleLugarer();
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

    let selectLugarer = (document.getElementById('selectLugarer')) as HTMLSelectElement;
    let options = selectLugarer.selectedOptions;
    var values = Array.from(options).map(({ value }) => value);

    let lugarer = ""
    lugarer += values[0];

    for (let i = 1; i < values.length; i++) {
      lugarer += "," + values[i];
    }

    // validering
    if (values.length < 1 || this.datoDag < 1 || this.datoDag > 31 || this.datoManed < 1 || this.datoManed > 12
      || this.datoAr < 2020 || this.datoAr > 2030 || this.datoTime < 0 || this.datoTime > 23 || this.datoMinutt < 0
      || this.datoMinutt > 59 || this.bilplasser < 0 || this.bilplasser > 1000
      || this.datoDag == null || this.datoManed == null || this.datoAr == null || this.datoTime == null || this.datoMinutt == null || this.bilplasser == null) {

      if (values.length < 1) {
        let feilmelding = (document.getElementById('feilmelding')) as HTMLElement;
        feilmelding.innerHTML = "Minst én lugar må legges til";
      }

      return;
    }

    this.http.post("api/Bestilling/lagreAvgang/" + baatNavn + "/" + strekningFra + "/" + strekningTil + "/" + this.datoDag.toString() + "/" + this.datoManed.toString() + "/" + this.datoAr.toString() + "/" + this.datoTime.toString() + "/" + this.datoMinutt + "/" + this.bilplasser + "/" + lugarer + "/" + this.aktiv.toString(), null)
      .subscribe(ok => {
        this.router.navigate(['/endreSlettAvgang']);
      },
        error => console.log(error)
      );
  }

  loggetInnSjekk() {
    this.http.get("api/Bestilling/isLoggedIn").
      subscribe(ok => {
      },
        error => {
          if (error.status == '401') {
            this.router.navigate(['/loggInn']);
          }
        }
      );
  }
}
