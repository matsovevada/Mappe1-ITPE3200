import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Kunde } from "../Kunde";
import { Billett } from "../Billett";
import { ViewEncapsulation } from "@angular/cli/lib/config/schema";

@Component({
  selector: 'kundeForm',
  templateUrl: "kundeForm.html",
  styleUrls: ["kundeFormStyle.css"],
  encapsulation: ViewEncapsulation.None
})


export class KundeForm implements OnInit {
  billett: Billett;
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

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private location: Location) {
    this.kundeSkjema = fb.group(this.validering);
    this.billett = this.router.getCurrentNavigation().extras.state.billett;
  }

  ngOnInit() {
  }


  onSubmit() {
    this.lagreKundeOgBillett();
  }

  lagreKundeOgBillett() {
    const lagretKunde = new Kunde();

    lagretKunde.fornavn = this.kundeSkjema.value.fornavn;
    lagretKunde.etternavn = this.kundeSkjema.value.etternavn;
    lagretKunde.adresse = this.kundeSkjema.value.adresse;
    lagretKunde.postnr = this.kundeSkjema.value.postnr;
    lagretKunde.poststed = this.kundeSkjema.value.poststed;
    lagretKunde.telefonnummer = this.kundeSkjema.value.tlf;
    lagretKunde.epost = this.kundeSkjema.value.epost;


    /*
     * Kunde lagres og returnert kundeID settes til billett. På dette tidspunktet har billett alle felter og kan lagres i DB.
     * BillettID sendes til neste side når kallene er ferdig
     */
    this.http.post<number>("api/Bestilling/lagreKunde", lagretKunde)
      .subscribe(lagretKundeId => {
        this.billett.kundeId = lagretKundeId;
        this.http.post<number>("api/Bestilling/lagreBillett", this.billett)
          .subscribe(billettLagretId => {
            this.router.navigate(['/visBillett/' + billettLagretId]);
          },
            error => console.log(error)
          );
      },
        error => console.log(error)
      );
  };
}

