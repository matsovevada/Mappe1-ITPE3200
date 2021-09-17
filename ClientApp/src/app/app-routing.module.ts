import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Bestilling } from './bestilling/bestilling';
import { LugarValg } from './lugarValg/lugarValg';
import { Test } from './test/Test';


const appRoots: Routes = [
  { path: '', component: Bestilling },
<<<<<<< HEAD
  { path: 'test/:id', component: Test },
=======
  { path: 'lugarValg', component: LugarValg },
>>>>>>> 91f5c7e6778b350538978ed6ae25a0b0dabfd3b5
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
