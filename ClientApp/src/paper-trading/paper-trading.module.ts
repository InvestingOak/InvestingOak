import {NgModule} from '@angular/core';
import {CommonModule, CurrencyPipe} from '@angular/common';
import {PaperTradingComponent} from './paper-trading.component';
import {RouterModule} from '@angular/router';
import {ProjectTreeComponent} from './project-tree/project-tree.component';
import {ProjectService} from './shared/project.service';
import {NewProjectComponent} from './new-project/new-project.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AuthorizeGuard} from '../api-authorization/authorize.guard';
import {ProjectComponent} from './project/project.component';
import {ProjectDeletedComponent} from './project-deleted/project-deleted.component';
import {ProjectSummaryComponent} from './project/project-summary/project-summary.component';
import {ProjectTradesComponent} from './project/project-trades/project-trades.component';
import {ProjectSettingsComponent} from './project/project-settings/project-settings.component';
import {NewTradeComponent} from './project/new-trade/new-trade.component';
import {OpenTradesComponent} from './project/open-trades/open-trades.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {StockDataService} from '../stock-data/stock-data.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {AuthorizeInterceptor} from '../api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    PaperTradingComponent,
    ProjectTreeComponent,
    NewProjectComponent,
    ProjectComponent,
    ProjectDeletedComponent,
    ProjectSummaryComponent,
    ProjectTradesComponent,
    ProjectSettingsComponent,
    NewTradeComponent,
    OpenTradesComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {
        path: 'paper', component: PaperTradingComponent, canActivate: [AuthorizeGuard], children: [
          {path: 'new', component: NewProjectComponent},
          {path: 'deleted', component: ProjectDeletedComponent},
          {path: 'project/:id', component: ProjectComponent}
        ]
      },
    ])
  ],
  providers: [
    CurrencyPipe,
    ProjectService,
    StockDataService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor,
      multi: true
    }
  ]
})
export class PaperTradingModule {
}
