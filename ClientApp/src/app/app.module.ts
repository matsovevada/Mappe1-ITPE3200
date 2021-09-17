import { NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms'
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { dropdownStrekning } from './strekning/dropdownStrekning'
import { BrowserModule } from '@angular/platform-browser'
import { Bestilling } from './bestilling/bestilling';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { KundeForm } from './kundeForm/kundeForm';

@NgModule({
  declarations: [
    AppComponent,
    dropdownStrekning,
    Bestilling,
    KundeForm,
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AppRoutingModule,
    RouterModule.forRoot([]),
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
