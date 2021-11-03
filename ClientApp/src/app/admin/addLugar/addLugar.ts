import { HttpClient } from "@angular/common/http";
import { Component, ViewEncapsulation } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Lugar } from "../../Lugar";


@Component({
  selector: 'add-lugar',
  templateUrl: 'addLugar.html',
  styleUrls: ["addLugarStyle.css"],
  encapsulation: ViewEncapsulation.None
})


export class AddLugar {
  nyLugarSkjema: FormGroup;
  feilMelding: String = "";

  validering = {
    id: [""],
    navn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,40}")])
    ],
    antSengeplasser: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,2}")])
    ],
    antLugarer: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}")])
    ],
    pris: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{2,5}")])
    ],
    beskrivelse: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,500}")])
    ],
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.nyLugarSkjema = fb.group(this.validering)
  }

  ngOnInit() {
    this.loggetInnSjekk();
  }

  onSubmit() {
    this.lagreLugar();
  }

  lagreLugar() {
    const lagretLugar = new Lugar()

    lagretLugar.navn = this.nyLugarSkjema.value.navn;
    lagretLugar.antallSengeplasser = this.nyLugarSkjema.value.antSengeplasser;
    lagretLugar.antallLedige = this.nyLugarSkjema.value.antLugarer;
    lagretLugar.antall = this.nyLugarSkjema.value.antLugarer;
    lagretLugar.pris = this.nyLugarSkjema.value.pris;
    lagretLugar.beskrivelse = this.nyLugarSkjema.value.beskrivelse;

    this.http.post<boolean>("api/Bestilling/lagreLugar/" + lagretLugar.navn + "/" + lagretLugar.beskrivelse + "/" + lagretLugar.antallSengeplasser + "/" + lagretLugar.antall + "/" + lagretLugar.antallLedige + "/" + lagretLugar.pris, null)
      .subscribe(lagret => {
        this.feilMelding = ""
        this.router.navigate(['/endreLugar']);
      },
        error => {
          console.log(error)
          this.feilMelding = "Kunne ikke lagre lugar. Sjekk informasjonen!"
        }
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
