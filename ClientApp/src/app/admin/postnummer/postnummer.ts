import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Strekning } from '../../Strekning';
import { ViewEncapsulation } from "@angular/core";
import { Postnummer } from "../../Postnummer";

@Component({
  selector: 'adminPostnummer',
  templateUrl: 'postnummer.html',
  styleUrls: [
    'StyleSheet.css'
  ],
  encapsulation: ViewEncapsulation.None,
})

export class AdminPostnummer {
  allePostnummere: Array<Postnummer>;
  laster: boolean = true;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.hentAllePostnummere()
  }

  hentAlleSPostnummere() {
    this.http.get<Postnummer[]>("api/Bestilling/hentPostnummer")
      .subscribe(postnummere => {
        this.allePostnummere = postnummere;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  slettPostnummer(postnummer) {
    console.log("Sletter postnummer: " + postnummer);
    this.http.delete('api/Bestilling/slettPostnummer/' + postnummer)
      .subscribe((ok) => {
        if (ok) {
          location.reload();
        }
      });
  }

  endrePostnummer(postnummer, poststed,) {

    this.http.put("api/Bestilling/endrePostnummer/" + postnummer + "/" + poststed, null)
      .subscribe(ok => {
        if (ok) {
          location.reload();
        }
      },
        error => console.log(error)
      );
  }

  lagrePostnummer(postnummer, poststed) {

    this.http.post("api/Bestilling/lagrePoststed/" + postnummer + "/" + poststed, null)
      .subscribe(ok => {
        if (ok) {
          location.reload();
        }
      },
        error => console.log(error)
      );
  }
}
