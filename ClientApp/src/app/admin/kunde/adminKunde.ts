import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Kunde } from '../../Kunde'


@Component({
  selector: 'adminKunde',
  templateUrl: 'adminKunde.html'
})

export class adminKunde {
  alleKunder: Array<Kunde>;
  laster: boolean;
  
  constructor(private http: HttpClient, private router: Router) {

  }


  ngOnInit() {
    this.hentAlleKunder();
  }

  hentAlleKunder() {
    this.http.get<Kunde[]>("api/Bestilling/hentKunder")
      .subscribe(kunder => {
        this.alleKunder = kunder;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

}
