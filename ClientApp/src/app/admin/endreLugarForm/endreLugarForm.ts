import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Lugar } from "../../Lugar";
import { ActivatedRoute, NavigationExtras } from '@angular/router';

@Component({
  selector: 'endre-lugar',
  templateUrl: 'endreLugarForm.html'
})

export class EndreLugarForm {
  nyLugarSkjema: FormGroup;
  feilMelding: String = "";
  valgtLugar: Lugar;
  lugarID: number;

/* Form validering er hentet og tilpasset fra eksempelprosjekt "Kunde-SPA-Routing" */
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

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private _ActivatedRoute: ActivatedRoute) {
    this.nyLugarSkjema = fb.group(this.validering)
  }

  ngOnInit() {
    this.loggetInnSjekk();
    this.lugarID = Number(this._ActivatedRoute.snapshot.paramMap.get('lugarId'));
    this.hentValgtLugar(this.lugarID);
  }

  hentValgtLugar(id) {
    this.http.get<Lugar>("api/Bestilling/hentLugar/" + id)
      .subscribe(lugar => {
        this.valgtLugar = lugar;
        this.nyLugarSkjema.patchValue({
          navn: this.valgtLugar.navn,
          antSengeplasser: this.valgtLugar.antallSengeplasser,
          antLugarer: this.valgtLugar.antall,
          pris: this.valgtLugar.pris,
          beskrivelse: this.valgtLugar.beskrivelse,
        })
      },
        error => console.log(error)
      );
  }

  onSubmit() {
    this.oppdaterLugar();
  }

  oppdaterLugar() {
    const id = this.lugarID;
    const navn = this.nyLugarSkjema.value.navn;
    const antallSengeplasser = this.nyLugarSkjema.value.antSengeplasser;
    const antLugarer = this.nyLugarSkjema.value.antLugarer;
    const pris = this.nyLugarSkjema.value.pris;
    const beskrivelse = this.nyLugarSkjema.value.beskrivelse;


    this.http.put<boolean>("api/Bestilling/endreLugar/" + id + "/" + navn + "/" + antallSengeplasser + "/" + antLugarer + "/" + pris + "/" + beskrivelse, null)
      .subscribe(ok => {
        if (ok) {
          this.feilMelding = ""
          this.router.navigate(['/endreLugar']);
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

}
