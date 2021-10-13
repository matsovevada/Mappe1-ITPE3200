import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Baat } from '../../Baat'


@Component({
  selector: 'adminBaat',
  templateUrl: 'adminBaat.html'
})

export class adminBaat {
  alleBaater: Array<Baat>;
  baatSlettet: boolean;
  laster: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleBaater();
    this.baatSlettet = false;
  }

  hentAlleBaater() {
    this.http.get<Baat[]>("api/Bestilling/hentBaater/")
      .subscribe(baater => {
        this.alleBaater = baater
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  slettValgtBaat(id) {
    this.http.delete<boolean>("api/Bestilling/slettBaat/" + id)
      .subscribe(slettet => {
        this.baatSlettet = slettet
      },
        error => console.log(error)
    );
    if (this.baatSlettet) { location.reload(); }
  }
}