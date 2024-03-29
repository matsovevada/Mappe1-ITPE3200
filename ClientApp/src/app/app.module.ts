import { NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms'
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser'
import { Bestilling } from './bestilling/bestilling';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { KundeForm } from './kundeForm/kundeForm';
import { LugarValg } from './lugarValg/lugarValg';
import { LugarValgRetur } from './lugarValgRetur/lugarValgRetur';
import { visBillett } from './visBillett/visBillett';
import { LugarComponent } from './lugarComponent/lugarComponent';
import { Navbar } from "./navbar/navbar";
import { Meny } from "./admin/meny/meny";
import { AdminStrekning } from './admin/strekning/strekning';
import { AdminPostnummer } from './admin/postnummer/postnummer';
import { AdminBaat } from './admin/baat/adminBaat';
import { AdminKunde } from './admin/kunde/adminKunde';
import { EndreKundeForm } from './admin/endreKundeForm/endreKundeForm';
import { AddAvgang } from './admin/addAvgang/addAvgang';
import { AddLugar } from './admin/addLugar/addLugar';
import { LoggInn } from './admin/loggInn/loggInn';
import { EndreSlettAvgang } from './admin/endreSlettAvgang/endreSlettAvgang';
import { EndreAvgang } from './admin/endreAvgang/endreAvgang';
import { EndreLugar } from './admin/endreLugar/endreLugar';
import { EndreLugarForm } from './admin/endreLugarForm/endreLugarForm';
import { SlettBillett } from './admin/slettBillett/slettBillett';

@NgModule({
  declarations: [
    AppComponent,
    Bestilling,
    KundeForm,
    LugarValg,
    LugarValgRetur,
    visBillett,
    LugarComponent,
    Navbar,
    Meny,
    AdminStrekning,
    AdminPostnummer,
    AdminBaat,
    AdminKunde,
    AddLugar,
    LoggInn,
    AddAvgang,
    AddLugar,
    EndreSlettAvgang,
    EndreAvgang,
    EndreKundeForm,
    EndreLugar,
    EndreLugarForm,
    SlettBillett
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
