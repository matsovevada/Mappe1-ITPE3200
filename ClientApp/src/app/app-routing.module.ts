import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminBaat } from './admin/baat/adminBaat';
import { Bestilling } from './bestilling/bestilling';
import { KundeForm } from './kundeForm/kundeForm';
import { LugarValg } from './lugarValg/lugarValg'
import { LugarValgRetur } from './lugarValgRetur/lugarValgRetur'
import { visBillett } from './visBillett/visBillett';
import { AdminStrekning } from './admin/strekning/strekning';
import { AdminPostnummer } from './admin/postnummer/postnummer';
import { EndreKundeForm } from './admin/endreKundeForm/endreKundeForm';
import { AdminKunde } from './admin/kunde/adminKunde';
import { Meny } from './admin/meny/meny';
import { AddLugar } from './admin/addLugar/addLugar';
import { LoggInn } from './admin/loggInn/loggInn';

const appRoots: Routes = [
  { path: '', component: Bestilling },
  { path: 'index', component: Bestilling },
  { path: 'lugarValg', component: LugarValg },
  { path: 'lugarValgRetur', component: LugarValgRetur },
  { path: '', redirectTo: 'bestilling', pathMatch: 'full' },
  { path: 'visBillett/:id', component: visBillett },
  { path: 'kundeForm', component: KundeForm },
  { path: 'adminStrekning', component: AdminStrekning },
  { path: 'adminPoststed', component: AdminPostnummer },
  { path: 'adminKunde', component: AdminKunde },
  { path: 'endreKundeForm', component: EndreKundeForm },
  { path: 'adminBaat', component: AdminBaat },
  { path: 'adminIndex', component: Meny },
  { path: 'adminAddLugar', component: AddLugar },
  { path: 'loggInn', component: LoggInn }
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
