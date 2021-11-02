import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Baat } from '../../Baat'


@Component({
  selector: 'adminBaat',
  templateUrl: 'adminBaat.html'
})

export class AdminBaat {
  alleBaater: Array<Baat>;
  baatSlettet: boolean;
  laster: boolean;
  endreValgt: boolean;
  feilInput: boolean;

  constructor(private http: HttpClient, private router: Router) {
  }

  ngOnInit() {
    this.laster = true;
    this.loggetInnSjekk();
    this.hentAlleBaater();
    this.baatSlettet = false;
    this.endreValgt = true;
  }

  hentAlleBaater() {
    this.http.get<Baat[]>("api/Bestilling/hentBaater/")
      .subscribe(baater => {
        this.alleBaater = baater
        this.laster = false;
      },
        error => {
          if (error.status == '401') {
            this.router.navigate(['/loggInn']);
          }
        }
      );
  }

  slettValgtBaat(id) {
    this.http.delete<boolean>("api/Bestilling/slettBaat/" + id)
      .subscribe(slettet => {
        this.baatSlettet = slettet
        if (this.baatSlettet) { location.reload(); }
      },
        error => console.log(error)
    );
    this.baatSlettet = false;
  }


  endreValgtBaat(innNavn, innId) {
    this.http.put<boolean>("api/Bestilling/endreBaat/" + innId + "/" + innNavn, null)
      .subscribe(endret => {
        location.reload();
      },
        error => console.log(error)
      );
  }

  lagreBaat(navn) {
    this.http.post<boolean>("api/Bestilling/lagreBaat/" + navn, null)
      .subscribe(lagre => {
        location.reload()
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

  valider(value) {
    if (value.length < 2 || value.length > 30) {
      this.feilInput = true;
    }
    else this.feilInput = false;
  }
}
