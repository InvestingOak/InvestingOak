import {Component, Input} from '@angular/core';
import {Observable} from 'rxjs';
import {CompanyProfile, Quote} from '../../../stock-data/responses';

@Component({
  selector: 'app-stock-summary',
  templateUrl: './stock-summary.component.html',
  styleUrls: ['./stock-summary.component.scss']
})
export class StockSummaryComponent {

  @Input()
  public symbol: string;

  @Input()
  public profile$: Observable<CompanyProfile>;  // Company profile

  @Input()
  public quote$: Observable<Quote>; // Stock quote from API

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
   * Expands and hides element when readMore link is clicked.
   * @param read Element to show/hide
   * @param readMore Link toggle
   */
  public onReadMore(read: Element, readMore: Element): void {
    read.classList.toggle('read-less');
    if (readMore.textContent == 'Read less') {
      readMore.textContent = 'Read more';
    } else {
      readMore.textContent = 'Read less';
    }
  }
}
