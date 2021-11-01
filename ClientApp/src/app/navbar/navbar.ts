import { Component } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'navbar',
  templateUrl: 'navbar.html'
})

export class Navbar {

  constructor(private http: HttpClient, private router: Router) { }

  loggUt() {
    this.http.get("api/Bestilling/loggUt").
      subscribe(ok => {
        console.log(ok)
        this.router.navigate(['/']);
      },
        error => {
          console.log(error)
        }
      );
  }
}
