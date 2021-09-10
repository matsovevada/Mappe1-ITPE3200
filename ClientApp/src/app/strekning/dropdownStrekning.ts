import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Strekning } from '../Strekning'

@Component({
  selector: 'strekning-dropdown',
  templateUrl: 'strekning.html'
})
export class Strekning {
  alleStrekninger: Array<Strekning>;
  laster: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleStrekninger();
  }

  hentAlleStrekninger() {
    this.http.get<Strekning[]>("api/strekninger/")
      .subscribe(strekningene => {
        this.alleStrekninger = strekningene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }
}
