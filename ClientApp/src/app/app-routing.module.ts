import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Bestilling } from './bestilling/bestilling';
import { LugarValg } from './lugarValg/lugarValg';
import { Test } from './test/Test';


const appRoots: Routes = [
  { path: '', component: Bestilling },
  { path: 'lugarValg', component: LugarValg },
  { path: '', redirectTo: 'bestilling', pathMatch: 'full' }
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
