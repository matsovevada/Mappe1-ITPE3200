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
      null, Validators.compose([Validators.required, Validators.pattern("")])
    ],
    tlf: [
      null, Validators.compose([Validators.required, Validators.pattern("")])
    ],
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private location:Location) {
    this.kundeSkjema = fb.group(this.validering);
  }

  ngOnInit() {
    // this.billett = this._ActivatedRoute.snapshot.paramMap.get(bill); 
      console.log("BILLLETTT!!");
      console.log(this.location.getState());  

  }


  onSubmit() {
    this.lagreKunde();
  }

  lagreKunde() {
    const lagretKunde = new Kunde();

    lagretKunde.fornavn = this.kundeSkjema.value.fornavn;
    lagretKunde.etternavn = this.kundeSkjema.value.etternavn;
    lagretKunde.adresse = this.kundeSkjema.value.adresse;
    lagretKunde.postnr = this.kundeSkjema.value.postnr;
    lagretKunde.poststed = this.kundeSkjema.value.poststed;
    lagretKunde.telefonnummer = this.kundeSkjema.value.tlf;
    lagretKunde.epost = this.kundeSkjema.value.epost;

    

    this.http.post<Number>("api/Bestilling/lagreKunde", lagretKunde)
      .subscribe(retur => {
        console.log(retur);
      },
        error => console.log(error)
      );
  };
}

