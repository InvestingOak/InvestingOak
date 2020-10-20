import {BrowserModule} from '@angular/platform-browser';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {NgModule} from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HomeComponent} from './home/home.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {NewsFeedComponent} from './news-feed/news-feed.component';
import {AdviserComponent} from './adviser/adviser.component';
import {TradingViewModule} from '../tradingview/tradingview.module';
import {ThemeSelectorComponent} from './theme-selector/theme-selector.component';
import {UserMenuComponent} from './user-menu/user-menu.component';
import {FinnhubService} from '../finnhub/finnhub.service';
import {StocksComponent} from './stocks/stocks.component';
import {StockHeaderComponent} from './stocks/stock-header/stock-header.component';
import {StockSummaryComponent} from './stocks/stock-summary/stock-summary.component';
import {StockAnalysisComponent} from './stocks/stock-analysis/stock-analysis.component';

@NgModule({
  declarations: [
    AdviserComponent,
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    NewsFeedComponent,
    ThemeSelectorComponent,
    UserMenuComponent,
    StocksComponent,
    StockHeaderComponent,
    StockSummaryComponent,
    StockAnalysisComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FontAwesomeModule,
    NgbModule,
    TradingViewModule,
  ],
  providers: [
    FinnhubService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
