import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TickerTapeComponent} from './ticker-tape/ticker-tape.component';
import {MiniChartComponent} from './mini-chart/mini-chart.component';
import {SymbolOverviewComponent} from './symbol-overview/symbol-overview.component';

@NgModule({
  declarations: [
    TickerTapeComponent,
    MiniChartComponent,
    SymbolOverviewComponent
  ],
  exports: [
    TickerTapeComponent,
    MiniChartComponent,
    SymbolOverviewComponent
  ],
  imports: [
    CommonModule
  ]
})
export class TradingViewModule { }
