import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Strekning } from './strekning/streking';


const appRoots: Routes = [
  { path: 'strekning', component: Strekning },
  { path: '', redirectTo: 'strekning', pathMatch: 'full' }
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
