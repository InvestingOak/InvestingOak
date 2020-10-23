import {Component, Input} from '@angular/core';
import {News} from '../../stock-data/responses';
import {Observable} from 'rxjs';
import * as _ from 'underscore';

@Component({
  selector: 'app-news-feed',
  templateUrl: './news-feed.component.html',
  styleUrls: ['./news-feed.component.scss']
})
export class NewsFeedComponent {

  // Input parameters
  @Input()
  public news: Observable<News[]>;  // News feed object

  @Input()
  public title: string; // Title to be displayed on top

  @Input()
  public usePagination = true;  // Whether to split into pages

  public page = 1;      // Current page
  public pageSize = 5;  // Max number of articles per page
  public maxSize = 10;  // Max number of pages shown in pagination bar

  /**
   * Filter news to remove unwanted articles.
   * Currently only removes duplicate headlines.
   * @param news
   */
  public filterNews(news: News[]): News[] {
    return _.uniq(news, n => n.headline);
  }

  /**
   * Returns time since a given time.
   * Examples: 5 minutes ago, 1 day ago, Just now
   * @param timestamp UNIX timestamp in seconds
   */
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
