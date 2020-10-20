import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {CompanyProfile2, News, Quote} from '../../finnhub/responses';
import {Observable} from 'rxjs';
import {FinnhubService} from '../../finnhub/finnhub.service';
import {IdType} from '../../finnhub/idType';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html'
})
export class StocksComponent implements OnInit {

  public symbol: string;  // The stock symbol

  public quote$: Observable<Quote>;    // Stock quote from API
  public profile$: Observable<CompanyProfile2>;  // Company profile
  public companyNews$: Observable<News[]>;  // News source from API

  public constructor(private finnhub: FinnhubService, private route: ActivatedRoute, private router: Router) {
    // Load data on page refresh (fixes navigating between symbols)
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.loadData();
      }
    });
  }

  public ngOnInit(): void {
    // Load data on page load
    this.loadData();
  }

  /**
   * Load stock data from APIs
   * @private
   */
  private loadData() {
    // Get stock symbol from URL
    this.symbol = this.route.snapshot.paramMap.get('symbol');

    // Get stock quote including open, close, high, low, and previous close prices.
    this.quote$ = this.finnhub.quote(this.symbol);

    // Get some info about the company.
    this.profile$ = this.finnhub.companyProfile2(IdType.Symbol, this.symbol);

    // Get company news since 30 days ago
    const today = new Date();
    const daysAgo = new Date(new Date().setDate(today.getDate() - 30));
    this.companyNews$ = this.finnhub.companyNews(this.symbol, daysAgo, today);
  }
}
