import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ViewEncapsulation } from "@angular/core";
import { Poststed } from "../../Poststed";

@Component({
  selector: 'adminPostnummer',
  templateUrl: 'postnummer.html',
  styleUrls: [
    'StyleSheet.css'
  ],
  encapsulation: ViewEncapsulation.None,
})

export class AdminPostnummer {
  allePoststeder: Array<Poststed>;
  laster: boolean = true;
  feilInputLagrePostnummer: boolean;
  feilInputLagrePoststed: boolean;
  feilInputEndre: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.loggetInnSjekk();
    this.hentAllePoststeder()
  }

  hentAllePoststeder() {
    this.http.get<Poststed[]>("api/Bestilling/hentPoststed")
      .subscribe(poststeder => {
        this.allePoststeder = poststeder;
        this.laster = false;
      },
        error => {
          if (error.status == '401') {
            this.router.navigate(['/loggInn']);
          }
        }
      );
  }

  slettPoststed(postnummer) {
    console.log("Sletter postnummer: " + postnummer);
    this.http.delete('api/Bestilling/slettPoststed/' + postnummer)
      .subscribe((ok) => {
        if (ok) {
          location.reload();
        }
      });
  }

  endrePoststed(postnummer, poststed) {

    this.http.put("api/Bestilling/endrePoststed/" + postnummer + "/" + poststed, null)
      .subscribe(ok => {
        if (ok) {
          location.reload();
        }
      },
        error => console.log(error)
      );
  }

  lagrePoststed(postnummer, poststed) {

    this.http.post("api/Bestilling/lagrePoststed/" + postnummer + "/" + poststed, null)
      .subscribe(ok => {
        if (ok) {
          location.reload();
        }
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

  validerLagrePostnummer(value) {
    if (value.length != 4 || isNaN(value)) {
      this.feilInputLagrePostnummer = true;
    }
    else this.feilInputLagrePostnummer = false;
  }

  validerLagrePoststed(value) {
    if (value.length < 2 || value.length > 30) {
      this.feilInputLagrePoststed = true;
    }
    else this.feilInputLagrePoststed = false;
  }

  validerEndre(value) {
    if (value.length < 2 || value.length > 30) {
      this.feilInputEndre = true;
    }
    else this.feilInputEndre = false;
  }
}
