import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Avgang } from '../Avgang'
import { Strekning } from '../Strekning'

@Component({
  selector: 'bestilling',
  templateUrl: 'bestilling.html'
})
export class Bestilling {
  alleAvganger: Array<Avgang>;
  alleStrekninger: Array<Strekning>;
  strekning: String;
  laster: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleStrekninger();
  }

  hentAlleAvganger() {
    this.http.get<Avgang[]>("api/Bestilling/hentAvgang/")
      .subscribe(avgangene => {
        console.log(avgangene)
        this.alleAvganger = avgangene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  hentAlleStrekninger() {
    this.http.get<Strekning[]>("api/Bestilling/hentStrekning")
      .subscribe(strekningene => {
        console.log(strekningene)
        this.alleStrekninger = strekningene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }
}
