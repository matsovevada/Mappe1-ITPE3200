import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Kunde } from "../Kunde";
import { Billett } from "../Billett";

@Component({
  selector: 'kundeForm',
  templateUrl: "kundeForm.html"
})


export class KundeForm implements OnInit {
  billett: Billett;
  kundeSkjema: FormGroup;
  feilMelding: String = "";
  

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
    console.log("BILLETTET I KUNDENE FORMEM");
    console.log(this.billett);
  }

  ngOnInit() {
    // this.billett = this._ActivatedRoute.snapshot.paramMap.get(bill); 
    /*   console.log("BILLLETTT!!");
     console.log(this.location.getState());*/


    /*const navigation = this.router.getCurrentNavigation();
  const state = navigation.extras.state as { billett: Billett };
  this.billett = state.billett;

  console.log(this.billett);*/


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

