import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { Strekning } from './strekning/dropdownStrekning'


@NgModule({
  declarations: [
    AppComponent,
    Strekning
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
