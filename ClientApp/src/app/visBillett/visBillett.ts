import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Kunde } from "../Kunde";
import { Billett } from "../Billett";
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'visBillett',
  templateUrl: "visBillett.html"
})

export class visBillett {
 billett: Billett;
 laster: boolean;

constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }


  ngOnInit() {
    //this.billett = this._ActivatedRoute.snapshot.paramMap.get(bill);
    const bill = new Billett();
    bill.avgangId = 1;
    bill.bilplass = true;
    bill.id = 1;
    bill.totalPris = 1000;
    bill.lugarer = null;

    this.billett = bill;
    
  }
}