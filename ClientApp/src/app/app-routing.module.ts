import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from './home/home.component';
import {AdviserComponent} from './adviser/adviser.component';
import {ApiAuthorizationModule} from '../api-authorization/api-authorization.module';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'adviser', component: AdviserComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    ApiAuthorizationModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
