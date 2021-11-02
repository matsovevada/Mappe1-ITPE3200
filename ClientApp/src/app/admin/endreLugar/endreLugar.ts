import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Lugar } from '../../Lugar'


@Component({
  selector: 'endreLugar',
  templateUrl: 'endreLugar.html'
})

export class EndreLugar {
  alleLugarer: Array<Lugar>;
  laster: boolean;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {}


  ngOnInit() {
    this.loggetInnSjekk();
    this.hentAlleLugarer();
  }

  hentAlleLugarer() {
    this.http.get<Lugar[]>("api/Bestilling/hentAlleLugarer")
      .subscribe(lugarer => {
        this.alleLugarer = lugarer;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  slettValgtLugar(id) {
    this.http.delete<boolean>("api/Bestilling/slettLugar/" + id)
      .subscribe(lugarSlettet => {
        if (lugarSlettet) { location.reload(); }
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
