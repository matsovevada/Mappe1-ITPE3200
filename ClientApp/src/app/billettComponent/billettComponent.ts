import { Component, OnInit, Input } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router'
import { Billett } from '../Billett';
import { Kunde } from '../Kunde';
import { Avgang } from '../Avgang';

@Component({
  selector: 'billett',
  templateUrl: 'billettComponent.html'
})

export class BillettComponent implements OnInit {

  @Input() billett!: Billett;
  @Input() kunde!: Kunde;
  @Input() retur!: Avgang;
  @Input() avgang!: Avgang;

  boolRetur: Boolean;
  avgangBilplass: String;
  returBilplass: String;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.checkRetur();
  }

  checkRetur() {
    if (this.retur) {
      this.boolRetur = true;
    }
    else {
      this.boolRetur = false;
    }
  }

  checkBilplass() {
    if (this.billett.avgangBilplass) {
      this.avgangBilplass = "Ja"
    }
    else {
      this.avgangBilplass = "Nei"
    }
    if (this.billett.returBilplass) {
      this.returBilplass = "Ja"
    }
    else {
      this.returBilplass = "Nei"
    }

  }
}
