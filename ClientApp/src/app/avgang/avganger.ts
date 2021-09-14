import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Avgang } from '../Avgang'

@Component({
  selector: 'avganger',
  templateUrl: 'avganger.html'
})
export class avganger {
  alleAvganger: Array<Avgang>;
  laster: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleAvganger();
  }

  hentAlleAvganger() {
    this.http.get<Avgang[]>("api/Bestilling/")
      .subscribe(avgangene => {
        console.log(avgangene)
        this.alleAvganger = avgangene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }
}
