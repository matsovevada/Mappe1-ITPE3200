import { HttpClient } from "@angular/common/http";
import { Component, ViewEncapsulation } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Bruker } from "../../Bruker";


@Component({
  selector: 'loggInn',
  templateUrl: 'loggInn.html',
  styleUrls: [
    'Stylesheet.css'
  ],
  encapsulation: ViewEncapsulation.None
})

export class LoggInn {
  loggInnSkjema: FormGroup;
  feilmelding: string = "";

  validering = {
    brukernavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    passord: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{6,}")])
    ]
  }
  
  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.loggInnSkjema = fb.group(this.validering);
  }

  resetFeilmelding() {
    console.log("Change")
    this.feilmelding = "";
  }

  onSubmit() {
    let bruker = new Bruker()
    bruker.brukernavn = this.loggInnSkjema.value.brukernavn;
    bruker.passord = this.loggInnSkjema.value.passord;

    this.http.post("api/Bestilling/loggInn", bruker)
      .subscribe(ok => {
        if (ok) {
          this.router.navigate(['/adminIndex']);
        }
        else {
          this.feilmelding = "Feil brukernavn eller passord";
        }
      },
        error => console.log(error)
      );

    }

  }

 

