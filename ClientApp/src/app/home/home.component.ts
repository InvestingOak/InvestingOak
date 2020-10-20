import {Component, OnInit} from '@angular/core';
import {FinnhubService} from '../../finnhub/finnhub.service';
import {News} from '../../finnhub/responses';
import {Observable} from 'rxjs';
import {Title} from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  public marketNews: Observable<News[]>;

  public constructor(private finnhub: FinnhubService, private title: Title) {
  }

  public ngOnInit(): void {
    this.title.setTitle('InvestingOak');
    this.marketNews = this.finnhub.marketNews('general');
  }
}
