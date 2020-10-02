import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NewsFeedComponent } from './news-feed/news-feed.component';
import { AdviserComponent } from './adviser/adviser.component';
import { TradingViewModule } from '../tradingview/tradingview.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    NewsFeedComponent,
    AdviserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TradingViewModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
