import { Component } from "@angular/core";

@Component({
  selector: 'meny',
  templateUrl: 'meny.html'
})

export class Meny {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
