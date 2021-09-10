import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppComponent } from './app.component';
import { dropdownStrekning } from './strekning/dropdownStrekning'


@NgModule({
  declarations: [
    AppComponent,
    dropdownStrekning,
    HttpClientModule
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
