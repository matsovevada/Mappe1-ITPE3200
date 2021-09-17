import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'test',
  templateUrl: 'test.html'
})
export class Test {
  
  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    
  }

}
