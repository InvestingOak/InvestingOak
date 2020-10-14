import {Component, OnInit} from '@angular/core';
import {FinnhubService} from '../../finnhub/finnhub.service';
import {News} from '../../finnhub/responses';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-news-feed',
  templateUrl: './news-feed.component.html',
   styleUrls: ['./news-feed.component.scss']
})
export class NewsFeedComponent implements OnInit {

  public marketNews: Observable<News[]>;

  public constructor(private finnhub: FinnhubService) { }

  public ngOnInit(): void {
    this.marketNews = this.finnhub.marketNews('general');
  }
}
