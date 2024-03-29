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
import { AddAvgang } from './admin/addAvgang/addAvgang';
import { AddLugar } from './admin/addLugar/addLugar';
import { EndreSlettAvgang } from './admin/endreSlettAvgang/endreSlettAvgang';
import { EndreAvgang } from './admin/endreAvgang/endreAvgang';
import { EndreLugar } from './admin/endreLugar/endreLugar';
import { EndreLugarForm } from './admin/endreLugarForm/endreLugarForm';
import { LoggInn } from './admin/loggInn/loggInn';
import { SlettBillett } from './admin/slettBillett/slettBillett';


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
  { path: 'adminAddAvgang', component: AddAvgang },
  { path: 'adminAddLugar', component: AddLugar },
  { path: 'endreSlettAvgang', component: EndreSlettAvgang },
  { path: 'endreAvgang', component: EndreAvgang },
  { path: 'adminAddLugar', component: AddLugar },
  { path: 'endreLugar', component: EndreLugar },
  { path: 'endreLugarForm', component: EndreLugarForm},
  { path: 'loggInn', component: LoggInn },
  { path: 'slettBillett', component: SlettBillett }
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
