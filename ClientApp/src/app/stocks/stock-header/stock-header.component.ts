import {Component, Input} from '@angular/core';
import {CompanyProfile, Quote} from '../../../stock-data/responses';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-stock-header',
  templateUrl: './stock-header.component.html',
  styleUrls: ['./stock-header.component.scss']
})
export class StockHeaderComponent {

  @Input()
  public symbol: string;  // The stock symbol

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
}
