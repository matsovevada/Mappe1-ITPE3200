import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Billett } from "../../Billett";


@Component({
  selector: 'slettBillett',
  templateUrl: 'slettBillett.html'
})

export class SlettBillett {
  alleBilletter: Array<Billett>;
  laster: boolean;
  endreValgt: boolean;

  constructor(private http: HttpClient, private router: Router) {
  }

  ngOnInit() {
    this.laster = true;
    this.hentAlleBilletter();
    this.endreValgt = false;
  }

  hentAlleBilletter() {
    this.http.get<Billett[]>("api/Bestilling/hentBilletter/")
      .subscribe(bill => {
        this.alleBilletter = bill;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  slettValgtBillett(id) {
    this.http.delete<boolean>("api/Bestilling/slettBillett/" + id)
      .subscribe(bill => {
      },
        error => console.log(error)
      );
  }

  }