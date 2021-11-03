import { Component, ViewEncapsulation } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'meny',
  templateUrl: 'meny.html',
  styleUrls: ['meny.css'],
  encapsulation: ViewEncapsulation.None
})

export class Meny {

  constructor(private http: HttpClient, private router: Router) { }

  //Sjekker om bruker er logget inn når siden lastes//
  ngOnInit() {
    this.http.get("api/Bestilling/isLoggedIn").
      subscribe(ok => {
      },
        error => {
          if (error.status == '401') {
            this.router.navigate(['/loggInn']);
          }
        }
      );
  }
}
