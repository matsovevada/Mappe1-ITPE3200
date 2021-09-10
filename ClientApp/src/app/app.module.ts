import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { dropdownStrekning } from './strekning/dropdownStrekning'


@NgModule({
  declarations: [
    AppComponent,
    dropdownStrekning
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
