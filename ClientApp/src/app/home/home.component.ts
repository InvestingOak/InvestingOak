import {Component, OnInit} from '@angular/core';
import {StockDataService} from '../../stock-data/stock-data.service';
import {News} from '../../stock-data/responses';
import {Observable} from 'rxjs';
import {Title} from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  public marketNews: Observable<News[]>;

  public constructor(private finnhub: StockDataService, private title: Title) {
  }

  public ngOnInit(): void {
    this.title.setTitle('InvestingOak');
    this.marketNews = this.finnhub.marketNews('general');
  }
}
