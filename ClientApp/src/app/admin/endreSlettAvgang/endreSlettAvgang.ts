import { Component, ViewEncapsulation } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Avgang } from "../../Avgang";

@Component({
  selector: 'endreSlettAvgang',
  templateUrl: 'endreSlettAvgang.html',
  styleUrls: ['StyleSheet.css'],
  encapsulation: ViewEncapsulation.None
})

export class EndreSlettAvgang {
  alleAvganger: Array<Avgang>;
  laster: boolean = true;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.loggetInnSjekk();
    this.hentAlleAvganger()
  }

  hentAlleAvganger() {
    this.http.get<Avgang[]>("api/Bestilling/hentAlleAvganger")
      .subscribe(avganger => {
        this.alleAvganger = avganger;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  slettAvgang(avgangId) {
    console.log("Sletter avgang med id " + avgangId);
    this.http.delete('api/Bestilling/slettAvgang/' + avgangId)
      .subscribe((ok) => {
        if (ok) {
          location.reload();
        }
      });
  }

  loggetInnSjekk() {
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
