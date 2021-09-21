import { Component, OnInit, Input } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Lugar } from '../Lugar'
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'lugar',
  templateUrl: 'lugarComponent.html'
})

export class LugarComponent {

  @Input() lugar!: Lugar;

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }

  ngOnInit(){

  }
}
