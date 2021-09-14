import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Strekning } from '../Strekning'

@Component({
  selector: 'strekning-dropdown',
  templateUrl: 'dropdownStrekning.html'
})
export class dropdownStrekning {
  alleStrekninger: Array<Strekning>;
  laster: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleStrekninger();
  }

  hentAlleStrekninger() {
    this.http.get<Strekning[]>("api/Bestilling/")
      .subscribe(strekningene => {
        console.log(strekningene)
        this.alleStrekninger = strekningene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }
}
