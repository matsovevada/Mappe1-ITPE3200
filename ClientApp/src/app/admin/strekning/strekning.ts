import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Strekning } from '../../Strekning';
import { ViewEncapsulation } from "@angular/core";

@Component({
  selector: 'adminStrekning',
  templateUrl: 'strekning.html',
  styleUrls: [
    'StyleSheet.css'
  ],
  encapsulation: ViewEncapsulation.None,
})

export class AdminStrekning {
  alleStrekninger: Array<Strekning>;
  laster: boolean = true;
  feilInputLagre: boolean;
  feilInputEndre: boolean;
  
  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.loggetInnSjekk();
    this.hentAlleStrekninger()
  }

  hentAlleStrekninger() {
    this.http.get<Strekning[]>("api/Bestilling/hentStrekning")
      .subscribe(strekningene => {
        this.alleStrekninger = strekningene;
        this.laster = false;
      },
      error => {
          if (error.status == '401') {
            this.router.navigate(['/loggInn']);
          }
        }
      );
  }

  slettStrekning(strekningId) {
    console.log("Sletter strekning med id " + strekningId);
    this.http.delete('api/Bestilling/slettStrekning/' + strekningId)
      .subscribe((ok) => {
        if (ok) {
          location.reload();
        }
      });
  }

  endreStrekning(strekningId, strekningFra, strekningTil) {
  
    this.http.put("api/Bestilling/endreStrekning/" + strekningId + "/" + strekningFra + "/" + strekningTil, null)
      .subscribe(ok => {
        if (ok) {
          location.reload();
        }
      },
        error => console.log(error)
      );
  }

  lagreStrekning(lagreStrekningFra, lagreStrekningTil) {

    this.http.post("api/Bestilling/lagreStrekning/" + lagreStrekningFra + "/" + lagreStrekningTil, null)
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

  validerLagre(value) {
    var pattern = /^[a-zA-Z]{2,30}$/
      if (!pattern.test(value)) {
         this.feilInputLagre = true;
      }
      else {
        this.feilInputLagre = false;
      }
  }

  validerEndre(value) {
    var pattern = /^[a-zA-Z]{2,30}$/
    if (!pattern.test(value)) {
      this.feilInputEndre = true;
    }
    else {
      this.feilInputEndre = false;
    }
  }
}
