import {Component, Input, OnInit} from '@angular/core';
import {CompanyProfile2, Quote, StockCandles} from '../../../finnhub/responses';
import {NavigationEnd, Router} from '@angular/router';
import {FinnhubService} from '../../../finnhub/finnhub.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-stock-header',
  templateUrl: './stock-header.component.html',
  styleUrls: ['./stock-header.component.scss']
})
export class StockHeaderComponent implements OnInit {

  @Input()
  public symbol: string;  // The stock symbol

  @Input()
  public profile$: Observable<CompanyProfile2>;  // Company profile

  @Input()
  public quote$: Observable<Quote>; // Stock quote from API

  public candles$: Observable<StockCandles>; // Latest candle

  public constructor(private finnhub: FinnhubService, private router: Router) {
    // Load data on page refresh (fixes navigating between symbols)
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.loadData();
      }
    });
  }

  public ngOnInit(): void {
    this.loadData();
  }

  public getExchangeAcronym(name: string): string {
    return this.finnhub.getExchangeAcronym(name);
  }

  /**
   * Get change in price.
   * @param current Current price
   * @param previous Previous price
   */
  public getChange(current: number, previous: number): { value: number, percent: number } {
    const change = current - previous;
    const percent = change / previous;

    return {value: change, percent: percent};
  }

  /**
   * Load stock data from APIs
   * @private
   */
  private loadData(): void {
    // Get chart candles from the last 3 days.
    const today = new Date();
    const daysAgo = new Date(new Date().setDate(today.getDate() - 3));
    this.candles$ = this.finnhub.stockCandles(this.symbol, 'D', daysAgo, today);
  }
}
