import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Kunde } from "../../Kunde";
import { ActivatedRoute, NavigationExtras } from '@angular/router';
import { ViewEncapsulation } from "@angular/cli/lib/config/schema";

@Component({
  selector: 'endreKundeForm',
  templateUrl: "endreKundeForm.html"
})


export class EndreKundeForm implements OnInit {
  kundeSkjema: FormGroup;
  feilMelding: String = "";
  kundeId: number;
  valgtKunde: Kunde;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private _ActivatedRoute: ActivatedRoute) {
    this.kundeSkjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.loggetInnSjekk();
    this.kundeId = Number(this._ActivatedRoute.snapshot.paramMap.get('kundeId'));
    this.hentValgtKunde(this.kundeId);
  }


  onSubmit() {
    this.oppdaterKunde();
  }

  oppdaterKunde() {
    const kunde = new Kunde();

    kunde.id = this.kundeId;
    kunde.fornavn = this.kundeSkjema.value.fornavn;
    kunde.etternavn = this.kundeSkjema.value.etternavn;
    kunde.adresse = this.kundeSkjema.value.adresse;
    kunde.postnr = this.kundeSkjema.value.postnr;
    kunde.poststed = this.kundeSkjema.value.poststed;
    kunde.telefonnummer = this.kundeSkjema.value.tlf;
    kunde.epost = this.kundeSkjema.value.epost;

    this.http.put<boolean>("api/Bestilling/endreKunde/", kunde)
      .subscribe(ok => {
        this.router.navigate(['/adminKunde']);
      },
        error => console.log(error)
      );      
  }

  hentValgtKunde(id) {
    this.http.get<Kunde>("api/Bestilling/hentKunde/" + id)
      .subscribe(kunde => {
        this.valgtKunde = kunde;
        this.kundeSkjema.patchValue({
          fornavn: this.valgtKunde.fornavn,
          etternavn: this.valgtKunde.etternavn,
          adresse: this.valgtKunde.adresse,
          postnr: this.valgtKunde.postnr,
          poststed: this.valgtKunde.poststed,
          epost: this.valgtKunde.epost,
          tlf: this.valgtKunde.telefonnummer,
        })
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
}


