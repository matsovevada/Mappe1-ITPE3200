import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Lugar } from '../Lugar'
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'lugar',
  templateUrl: 'lugarComponent.html'
})

export class LugarComponent implements OnInit {

  @Input() lugar!: Lugar;
  @Output() onAddLugar: EventEmitter<Lugar> = new EventEmitter();

  constructor(private http: HttpClient, private router: Router, private _ActivatedRoute: ActivatedRoute) { }


  ngOnInit(): void {
  }

  addLugar(lugar) {
    this.onAddLugar.emit(lugar);
  }
}
