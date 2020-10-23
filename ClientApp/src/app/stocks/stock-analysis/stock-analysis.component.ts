import {Component, Input, OnInit} from '@angular/core';
import {NewsSentiment, PriceTarget, Quote, Recommendations} from '../../../stock-data/responses';
import {Observable} from 'rxjs';
import {StockDataService} from '../../../stock-data/stock-data.service';
import {NavigationEnd, Router} from '@angular/router';

@Component({
  selector: 'app-stock-analysis',
  templateUrl: './stock-analysis.component.html',
})
export class StockAnalysisComponent implements OnInit {

  @Input()
  public symbol: string;

  @Input()
  public quote$: Observable<Quote>;

  public recommendation$: Observable<Recommendations>;  // Analyst buy/sell ratings
  public priceTarget$: Observable<PriceTarget>;  // Analyst price targets
  public newsSentiment$: Observable<NewsSentiment>;  // Market sentiment from news

  public constructor(private dataService: StockDataService, private router: Router) {
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
   * Generate a buy/sell rating based on analyst ratings.
   * @param recommendations Analyst ratings
   */
  public getMajorityRecommendation(recommendations: Recommendations): string {
    const totalBuys = recommendations.strongBuy + recommendations.buy;
    const totalSells = recommendations.strongSell + recommendations.sell;
    const holds = recommendations.hold;

    if (totalBuys > totalSells) {
      // Buy
      if (Math.abs(totalBuys - holds) / holds <= 0.5) {
        return 'Hold';
      }

      if ((recommendations.strongBuy - recommendations.buy) / recommendations.buy > 0.5) {
        return 'Buy';
      }

      return 'Outperform';
    } else if (totalSells > totalBuys) {
      // Sell
      if (Math.abs(totalSells - holds) / holds <= 0.5) {
        return 'Hold';
      }

      if ((recommendations.strongSell - recommendations.sell) / recommendations.sell > 0.5) {
        return 'Sell';
      }

      return 'Underperform';
    } else {
      return 'Hold';
    }
  }

  /**
   * Returns a Bootstrap 4 color based on the buy/sell rating.
   * @param name Buy/sell rating text
   */
  public getRecommendationColor(name: string): string {
    switch (name) {
      case 'Outperform':
        return 'primary';
      case 'Buy':
        return 'success';
      case 'Underperform':
        return 'warning';
      case 'Sell':
        return 'danger';
      default:
        return 'secondary';
    }
  }

  /**
   * Returns a Bootstrap 4 color based on price target compared with current price.
   * @param target Price target
   * @param current Current price
   */
  public getTargetColor(target: number, current: number): string {
    if ((target - current) / current > 0.1) {
      return 'success';
    }
    if ((target - current) / current < -0.1) {
      return 'warning';
    }
    return 'secondary';
  }

  /**
   * Returns the total number of analyst recommendations.
   * @param recommendations Analyst recommendations
   */
  public getRecommendationCount(recommendations: Recommendations): number {
    return recommendations.strongBuy + recommendations.buy +
      recommendations.hold + recommendations.sell + recommendations.strongSell;
  }

  /**
   * Generates a sentiment score.
   * @param score Company score
   * @param average Industry average score
   * @param bullish Bullish outlook percentage
   */
  public getSentiment(score: number, average: number, bullish: number): number {
    const sigDiff = Math.abs(score - average) / average > 0.1;
    let myScore = 0;

    if (sigDiff && score > average) {
      myScore++;
    }
    if (bullish > 0.6) {
      myScore++;
    }

    if (sigDiff && score < average) {
      myScore--;
    }
    if (bullish < 0.4) {
      myScore--;
    }

    return myScore;
  }

  /**
   * Returns a string representation of the sentiment score.
   * @param score Sentiment score
   */
  public getSentimentString(score: number): string {
    if (score > 0) {
      return 'Bullish';
    }
    if (score < 0) {
      return 'Bearish';
    }

    return 'Neutral';
  }

  /**
   * Returns a Bootstrap 4 color based on sentiment score.
   * @param sentiment Sentiment score
   */
  public getSentimentColor(sentiment: number): string {
    if (sentiment > 0) {
      return 'success';
    }
    if (sentiment < 0) {
      return 'danger';
    }
    return 'secondary';
  }

  /**
   * Load stock data from APIs
   * @private
   */
  private loadData() {
    this.recommendation$ = this.dataService.recommendation(this.symbol);
    this.priceTarget$ = this.dataService.priceTarget(this.symbol);
    this.newsSentiment$ = this.dataService.newsSentiment(this.symbol);
  }
}
