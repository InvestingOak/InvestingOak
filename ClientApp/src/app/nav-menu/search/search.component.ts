import {Component, OnInit} from '@angular/core';
import {faSearch} from '@fortawesome/free-solid-svg-icons';
import {Observable} from 'rxjs';
import {StockSymbol} from '../../../stock-data/responses';
import {Router} from '@angular/router';
import {StockDataService} from '../../../stock-data/stock-data.service';
import * as _ from 'underscore';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  public search = faSearch;
  public symbols$: Observable<StockSymbol[]>;
  public suggestions: StockSymbol[] = [];
  public showSuggestions = false;
  public numberSuggestions = 4;
  public input = '';

  public constructor(public router: Router, private dataService: StockDataService) {
  }

  public ngOnInit(): void {
    this.symbols$ = this.dataService.symbols('US');
  }

  public suggest(): void {
    if (!this.input) {
      this.suggestions = [];
      return;
    }

    this.symbols$.subscribe(symbols => {
      const normInput = this.input.toUpperCase();

      // First, suggest symbols that match input
      this.suggestions = _.unique(symbols.filter(s => {
        return s.displaySymbol.startsWith(normInput);
      }) // Seconds, suggest symbols with names that match input
        .concat(symbols.filter(s => {
          return s.description.startsWith(normInput);
        })) // Third, suggest names that include input, but don't match
        .concat(symbols.filter(s => {
          return s.description.includes(normInput);
        })) // Third suggest symbols that include input, but don't match
        .concat(symbols.filter(s => {
          return s.displaySymbol.includes(normInput);
        })) // Remove symbols with empty description
        .filter(s => s.description));
    });
  }

  public onEnter(): void {
    if (this.input === '' || !this.suggestions.find(s => s.symbol === this.input.toUpperCase())) {
      return;
    }

    const symbol = this.suggestions[0].symbol;
    this.router.navigate(['stocks', symbol]).then();
  }
}
