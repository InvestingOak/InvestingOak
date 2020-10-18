import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {CompanyProfile2, News, NewsSentiment, PriceTarget, Quote, RecommendationTrend, StockCandles} from '../../finnhub/responses';
import {Observable} from 'rxjs';
import {FinnhubService} from '../../finnhub/finnhub.service';
import {IdType} from '../../finnhub/idType';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.scss']
})
export class StocksComponent implements OnInit {

  public companyNews$: Observable<News[]>;

  public loadSuccessful = true;
  public symbol: string;
  public quote: Quote;
  public change: {
    value: number;
    percent: number;
  };
  public todayCandle: StockCandles;
  public profile: CompanyProfile2;
  public recommendations: RecommendationTrend[];
  public priceTarget: PriceTarget;
  public newsSentiment: NewsSentiment;
  public peers: string[] = [];

  public constructor(private finnhub: FinnhubService, private route: ActivatedRoute, private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.loadData();
      }
    });
  }

  public ngOnInit(): void {
    this.loadData();
  }

  public onReadMore(read: Element, readMore: Element): void {
    read.classList.toggle('read-less');
    if (readMore.textContent == 'Read less') {
      readMore.textContent = 'Read more';
    } else {
      readMore.textContent = 'Read less';
    }
  }

  public getChange(current: number, previous: number): number {
    return current - previous;
  }

  public getChangePercent(current: number, previous: number): number {
    const change = this.getChange(current, previous);
    return change / previous;
  }

  public getMajorityRecommendation(recommendations: RecommendationTrend): string {
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

  public getTargetColor(target: number, current: number): string {
    if ((target - current) / current > 0.1) {
      return 'success';
    }
    if ((target - current) / current < -0.1) {
      return 'warning';
    }
    return 'secondary';
  }

  public getRecommendationCount(recommendations: RecommendationTrend): number {
    return recommendations.strongBuy + recommendations.buy +
      recommendations.hold + recommendations.sell + recommendations.strongSell;
  }

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

  public getSentimentString(score: number): string {
    if (score > 0) {
      return 'Bullish';
    }
    if (score < 0) {
      return 'Bearish';
    }

    return 'Neutral';
  }

  public getSentimentColor(sentiment: number): string {
    if (sentiment > 0) {
      return 'success';
    }
    if (sentiment < 0) {
      return 'danger';
    }
    return 'secondary';
  }

  private loadData() {
    this.symbol = this.route.snapshot.paramMap.get('symbol');

    const today = new Date();
    this.companyNews$ = this.finnhub.companyNews(this.symbol, this.getDaysAgo(today, 30), today);

    this.finnhub.quote(this.symbol)
      .subscribe(q => {
        this.quote = q;
        this.change = {
          value: this.getChange(q.c, q.pc),
          percent: this.getChangePercent(q.c, q.pc)
        };
      });

    this.finnhub.stockCandles(this.symbol, 'D', this.getDaysAgo(today, 3), today)
      .subscribe(c => this.todayCandle = c);

    this.finnhub.companyProfile2(IdType.Symbol, this.symbol)
      .subscribe(p => {
        this.profile = p;
        this.profile.exchange = this.finnhub.getExchangeFromResponse(p.exchange);
      });
    this.finnhub.recommendationTrends(this.symbol)
      .subscribe(r => this.recommendations = r);
    this.finnhub.priceTarget(this.symbol)
      .subscribe(pt => this.priceTarget = pt);
    this.finnhub.newsSentiment(this.symbol)
      .subscribe(ns => this.newsSentiment = ns);
    this.finnhub.peers(this.symbol)
      .subscribe(p => this.peers = p);
  }

  private getDaysAgo(today: Date, days: number): Date {
    return new Date(new Date().setDate(today.getDate() - days));
  }
}
