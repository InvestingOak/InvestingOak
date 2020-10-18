import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';
import {AdviserComponent} from './adviser/adviser.component';
import {ApiAuthorizationModule} from '../api-authorization/api-authorization.module';
import {NotFoundComponent} from '../errors/not-found/not-found.component';
import {ErrorsModule} from '../errors/errors.module';
import {StocksComponent} from './stocks/stocks.component';

const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'adviser', component: AdviserComponent},
  {path: 'stocks/:symbol', component: StocksComponent},
  {path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    ApiAuthorizationModule,
    ErrorsModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
