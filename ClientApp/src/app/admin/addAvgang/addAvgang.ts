import { Component, ViewEncapsulation } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Baat } from '../../Baat';
import { Lugar } from '../../Lugar';
import { Strekning } from '../../Strekning';
import { Avgang } from '../../Avgang';

@Component({
  selector: 'addAvgang',
  templateUrl: 'addAvgang.html',
  styleUrls: ['StyleSheet.css'],
  encapsulation: ViewEncapsulation.None
})

export class AddAvgang {
  laster: boolean = false;
  alleBaater: Array<Baat>
  alleStrekninger: Array<Strekning>
  alleLugarer: Array<Lugar>
 
  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleBaater();
    this.hentAlleStrekninger();
    this.hentAlleLugarer();
  }

  hentAlleBaater() {
    this.http.get<Baat[]>("api/Bestilling/hentBaater")
      .subscribe(baatene => {
        this.alleBaater = baatene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  hentAlleStrekninger() {
    this.http.get<Strekning[]>("api/Bestilling/hentStrekning")
      .subscribe(strekningene => {
        this.alleStrekninger = strekningene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  hentAlleLugarer() {
    this.http.get<Lugar[]>("api/Bestilling/hentAlleLugarer")
      .subscribe(lugarene => {
        console.log("LUG:")
        console.log(lugarene)
        this.alleLugarer = lugarene;
        this.laster = false;
      },
        error => console.log(error)
      );
  }

  selectChangeHandler(e) {
    console.log(e);
  }

  submit() {
    let element = (document.getElementById('selectLugarer')) as HTMLSelectElement;
    let options = element.selectedOptions;
    var values = Array.from(options).map(({ value }) => value);
    console.log(values);

    var e = document.getElementById("selectBaat") as HTMLSelectElement;
    var strUser = e.value;

    console.log(strUser);

    let avgang = new Avgang();

    let object = {
      "a": "some value",
      "b": "another value"
    };

    this.http.post("api/Bestilling/lagreAvgang", JSON.stringify(object))
      .subscribe(ok => {
        console.log(ok);
      },
        error => console.log(error)
      );
  }
}
