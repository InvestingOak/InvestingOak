import {Component, OnInit} from '@angular/core';
import {faSearch} from '@fortawesome/free-solid-svg-icons';
import {Observable} from 'rxjs';
import {StockSymbol} from '../../../finnhub/responses';
import {Router} from '@angular/router';
import {FinnhubService} from '../../../finnhub/finnhub.service';

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

  public constructor(public router: Router, private finnhub: FinnhubService) {
  }

  public ngOnInit(): void {
    this.symbols$ = this.finnhub.symbols('US');
  }

  public suggest(input: string): void {
    if (!input) {
      this.suggestions = [];
      return;
    }

    this.symbols$.subscribe(symbols => {
      this.suggestions = symbols.filter(s => {
        const normInput = input.toUpperCase();
        return s.displaySymbol.includes(normInput) || s.description.includes(normInput);
      }).slice(0, 4);
    });
  }
}
