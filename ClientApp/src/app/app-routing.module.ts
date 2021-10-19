import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Bestilling } from './bestilling/bestilling';
import { KundeForm } from './kundeForm/kundeForm';
import { LugarValg } from './lugarValg/lugarValg'
import { LugarValgRetur } from './lugarValgRetur/lugarValgRetur'
import { visBillett } from './visBillett/visBillett';
import { AdminStrekning } from './admin/strekning/strekning';
import { AdminPostnummer } from './admin/postnummer/postnummer';


const appRoots: Routes = [
  { path: '', component: Bestilling },
  { path: 'index', component: Bestilling },
  { path: 'lugarValg', component: LugarValg },
  { path: 'lugarValgRetur', component: LugarValgRetur },
  { path: '', redirectTo: 'bestilling', pathMatch: 'full' },
  { path: 'visBillett/:id', component: visBillett },
  { path: 'kundeForm', component: KundeForm },
  { path: 'adminStrekning', component: AdminStrekning },
  { path: 'adminPostnummer', component: AdminPostnummer }
  { path: 'test', component: adminKunde },
  { path: 'endreKundeForm', component: EndreKundeForm },
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
