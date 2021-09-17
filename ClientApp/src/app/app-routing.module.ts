import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Bestilling } from './bestilling/bestilling';
import { Test } from './test/Test';


const appRoots: Routes = [
  { path: '', component: Bestilling },
  { path: 'test/:id', component: Test },
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
