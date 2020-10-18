import {Component, Input} from '@angular/core';
import {News} from '../../finnhub/responses';
import {Observable} from 'rxjs';
import * as _ from 'underscore';

@Component({
  selector: 'app-news-feed',
  templateUrl: './news-feed.component.html',
  styleUrls: ['./news-feed.component.scss']
})
export class NewsFeedComponent {

  public page = 1;

  @Input()
  public news: Observable<News[]>;

  @Input()
  public title: string;

  @Input()
  public usePagination = true;

  public pageSize = 5;

  public maxSize = 10;

  public filterNews(news: News[]): News[] {
    return _.uniq(news, n => n.headline);
  }

  public getAge(timestamp: number): string {
    const seconds = Math.floor((+new Date() - +new Date(timestamp * 1000)) / 1000);
    if (seconds < 29) {
      return 'Just now';
    }
    const intervals = {
      'year': 31536000,
      'month': 2592000,
      'week': 604800,
      'day': 86400,
      'hour': 3600,
      'minute': 60,
      'second': 1
    };
    let counter;
    for (const i in intervals) {
      counter = Math.floor(seconds / intervals[i]);
      if (counter > 0) {
        if (counter === 1) {
          return `${counter} ${i} ago`;
        } else {
          return `${counter} ${i}s ago`;
        }
      }
    }
  }
}
