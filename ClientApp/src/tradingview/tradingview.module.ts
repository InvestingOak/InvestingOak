import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {TickerTapeComponent} from './ticker-tape/ticker-tape.component';
import { MiniChartComponent } from './mini-chart/mini-chart.component';

@NgModule({
  declarations: [
    TickerTapeComponent,
    MiniChartComponent
  ],
  exports: [
    TickerTapeComponent,
    MiniChartComponent
  ],
  imports: [
    CommonModule
  ]
})
export class TradingViewModule { }
