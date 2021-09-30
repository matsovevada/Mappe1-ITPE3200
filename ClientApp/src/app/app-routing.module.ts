import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Bestilling } from './bestilling/bestilling';
import { KundeForm } from './kundeForm/kundeForm';
import { LugarValg } from './lugarValg/lugarValg'
import { LugarValgRetur } from './lugarValgRetur/lugarValgRetur'
import { visBillett } from './visBillett/visBillett';


const appRoots: Routes = [
  { path: '', component: Bestilling },
  { path: 'lugarValg', component: LugarValg },
  { path: 'lugarValgRetur', component: LugarValgRetur },
  { path: '', redirectTo: 'bestilling', pathMatch: 'full' },
  { path: 'visBillett/:id', component: visBillett },
  { path: 'kundeForm', component: KundeForm }
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
