import { Component, OnInit, Input } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Lugar } from '../Lugar'
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'lugar',
  templateUrl: 'lugar.html'
})

export class Lugar {

  @Input() lugar!: Lugar;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit(){

  }
}
