import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Kunde } from "../Kunde";
import { Billett } from "../Billett";
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'visBillett',
  templateUrl: "visBillett.html"
})

export class visBillett {
  billett: Billett;
  billettId: String = "";

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute, private location: Location) {
    
  }

  ngOnInit() {
    this.billettId = this._ActivatedRoute.snapshot.paramMap.get('id');
    this.hentBillett();
   
   
   
  }

  hentBillett() {
    this.http.get<Billett>("api/Bestilling/hentBillett/" + this.billettId).
      subscribe(hentetBillett => {
        this.billett = hentetBillett;
        console.log("BIIIILLLLLL!!!!1");
        console.log(this.billett);
      },
        error => console.log(error)
      );
  }

  
}
