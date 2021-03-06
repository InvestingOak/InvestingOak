import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {Observable} from 'rxjs';
import {StockDataService} from '../../stock-data/stock-data.service';
import {Title} from '@angular/platform-browser';
import {CompanyProfile, News, Quote} from '../../stock-data/responses';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html'
})
export class StocksComponent implements OnInit {

  public symbol: string;  // The stock symbol

  public quote$: Observable<Quote>;    // Stock quote from API
  public profile$: Observable<CompanyProfile>;  // Company profile
  public companyNews$: Observable<News[]>;  // News source from API

  public constructor(private dataService: StockDataService, private route: ActivatedRoute,
                     private router: Router, private titleService: Title) {
    // Load data on page refresh (fixes navigating between symbols)
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.loadData();
      }
    });
  }

  public ngOnInit(): void {
    this.titleService.setTitle(`${this.symbol} · InvestingOak`);

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
    this.quote$ = this.dataService.quote(this.symbol);

    // Get some info about the company.
    this.profile$ = this.dataService.companyProfile(this.symbol);

    // Get company news since 30 days ago
    const today = new Date();
    const daysAgo = new Date(new Date().setDate(today.getDate() - 30));
    this.companyNews$ = this.dataService.companyNews(this.symbol, daysAgo, today);
  }
}
