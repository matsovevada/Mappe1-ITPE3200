import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'test',
  templateUrl: 'test.html'
})
export class Test {

  id: string;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.id = this._ActivatedRoute.snapshot.paramMap.get("id");
    console.log(this.id);
  }

}
