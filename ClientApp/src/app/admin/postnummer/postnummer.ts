import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ViewEncapsulation } from "@angular/core";
import { Poststed } from "../../Poststed";

@Component({
  selector: 'adminPostnummer',
  templateUrl: 'postnummer.html',
  styleUrls: [
    'StyleSheet.css'
  ],
  encapsulation: ViewEncapsulation.None,
})

export class AdminPostnummer {
  allePoststeder: Array<Poststed>;
  laster: boolean = true;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.hentAllePoststeder()
  }

  hentAllePoststeder() {
    this.http.get<Poststed[]>("api/Bestilling/hentPoststed")
      .subscribe(poststeder => {
        this.allePoststeder = poststeder;
        console.log("POSTSTEDER:")
        console.log(poststeder);
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  slettPoststed(postnummer) {
    console.log("Sletter postnummer: " + postnummer);
    this.http.delete('api/Bestilling/slettPoststed/' + postnummer)
      .subscribe((ok) => {
        if (ok) {
          location.reload();
        }
      });
  }

  endrePoststed(postnummer, poststed) {

    this.http.put("api/Bestilling/endrePoststed/" + postnummer + "/" + poststed, null)
      .subscribe(ok => {
        if (ok) {
          location.reload();
        }
      },
        error => console.log(error)
      );
  }

  lagrePoststed(postnummer, poststed) {

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
