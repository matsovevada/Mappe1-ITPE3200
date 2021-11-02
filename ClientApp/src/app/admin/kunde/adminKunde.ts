import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Kunde } from '../../Kunde'


@Component({
  selector: 'adminKunde',
  templateUrl: 'adminKunde.html'
})

export class AdminKunde {
  alleKunder: Array<Kunde>;
  laster: boolean;
  kundeSkjema: FormGroup;
  feilMelding: String = "";

  /* Form er hentet og tilpasset fra eksempel prosjekt "Kunde-SPA-Routing" */
  validering = {
    id: [""],
    fornavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    etternavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    adresse: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    postnr: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])
    ],
    poststed: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    epost: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-z0-9!#$ %& '*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")])
    ],
    tlf: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{8}")])
    ],
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.kundeSkjema = fb.group(this.validering);
  }


  ngOnInit() {
    this.loggetInnSjekk();
    this.hentAlleKunder();
  }

  hentAlleKunder() {
    this.http.get<Kunde[]>("api/Bestilling/hentKunder")
      .subscribe(kunder => {
        this.alleKunder = kunder;
        this.laster = false;
      },
        error => {
          if (error.status == '401') {
            this.router.navigate(['/loggInn']);
          }
        }
      );
  }

  slettValgtKunde(id) {
    this.http.delete<boolean>("api/Bestilling/slettKunde/" + id)
      .subscribe(kundeSlettet => {
        if (kundeSlettet) { location.reload(); }
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
