import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { dropdownStrekning } from './strekning/dropdownStrekning';


const appRoots: Routes = [
  { path: 'dropdownStrekning', component: dropdownStrekning },
  { path: '', redirectTo: 'dropdownStrekning', pathMatch: 'full' }
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
