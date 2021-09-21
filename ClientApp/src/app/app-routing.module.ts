import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Bestilling } from './bestilling/bestilling';
import { KundeForm } from './kundeForm/kundeForm';
import { LugarValg } from './lugarValg/lugarValg'
import { visBillett } from './visBillett/visBillett';


const appRoots: Routes = [
  { path: '', component: Bestilling },
  { path: 'lugarValg/:id', component: LugarValg },
  { path: '', redirectTo: 'bestilling', pathMatch: 'full' },
  { path: 'visBillett/:billett', component: visBillett }
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
