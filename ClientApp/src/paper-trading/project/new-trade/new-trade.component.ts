import {Component, OnInit} from '@angular/core';
import {ProjectService} from '../../shared/project.service';
import {Project} from '../../shared/project';
import {StockDataService} from '../../../stock-data/stock-data.service';
import {StockSymbol} from '../../../stock-data/responses';

@Component({
  selector: 'app-new-trade',
  templateUrl: './new-trade.component.html',
  styleUrls: ['./new-trade.component.scss']
})
export class NewTradeComponent implements OnInit {

  private symbols: StockSymbol[];

  public trade = {
    symbol: '',
    quantity: 0,
    expiration: Date,
    strike: 0,
    price: 0
  };

  public constructor(public stockDataService: StockDataService, private projectService: ProjectService) {
  }

  public get project(): Project {
    return this.projectService.project;
  }

  public get symbolDescription(): string {
    if (this.trade.symbol && this.symbols) {
      const result = this.symbols.find(s => s.symbol === this.trade.symbol);
      return result ? result.description : 'Symbol does not exist.';
    }
    return '';
  }

  public ngOnInit(): void {
    this.stockDataService.symbols('US').subscribe(s => {
      this.symbols = s;
    });
  }
}
