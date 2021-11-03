import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Billett } from '../../Billett'


@Component({
  selector: 'slettBillett',
  templateUrl: 'slettBillett.html',
  styleUrls: ["slettBillettStyle.css"],
  encapsulation: ViewEncapsulation.None
})

export class SlettBillett {
  alleBilletter: Array<Billett>;
  laster: boolean;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) { }


  ngOnInit() {
    this.loggetInnSjekk();
    this.hentAlleBilletter();
  }

  hentAlleBilletter() {
    this.http.get<Billett[]>("api/Bestilling/hentAlleBilletter")
      .subscribe(billetter => {
        console.log(billetter);
        this.alleBilletter = billetter;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  slettValgtBillett(id) {
    this.http.delete<boolean>("api/Bestilling/slettBillett/" + id)
      .subscribe(bill => {
        if (bill) { location.reload(); }
      },
        error => console.log(error)
      );
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
