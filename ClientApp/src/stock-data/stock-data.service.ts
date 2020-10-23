import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CompanyProfile, News, NewsSentiment, PriceTarget, Quote, Recommendations, StockSymbol} from './responses';

@Injectable({
  providedIn: 'root'
})
export class StockDataService {

  public constructor(private http: HttpClient) {
  }

  public companyProfile(symbol: string): Observable<CompanyProfile> {
    return this.http.get<CompanyProfile>(`/api/stocks/${symbol}/profile`);
  }

  public symbols(exchange: string): Observable<StockSymbol[]> {
    return this.http.get<StockSymbol[]>(`/api/stocks?exchange=${exchange}`);
  }

  public marketNews(category: 'general' | 'forex' | 'crypto' | 'merger', minId = 0): Observable<News[]> {
    return this.http.get<News[]>(`api/news/${category}?minId=${minId}`);
  }

  public companyNews(symbol: string, from: Date, to: Date): Observable<News[]> {
    return this.http.get<News[]>(`api/stocks/${symbol}/news?from=${from.toUTCString()}&to=${to.toUTCString()}`);
  }

  public newsSentiment(symbol: string): Observable<NewsSentiment> {
    return this.http.get<NewsSentiment>(`api/stocks/${symbol}/sentiment`);
  }

  public recommendation(symbol: string): Observable<Recommendations> {
    return this.http.get<Recommendations>(`api/stocks/${symbol}/recommendation`);
  }

  public priceTarget(symbol: string): Observable<PriceTarget> {
    return this.http.get<PriceTarget>(`api/stocks/${symbol}/pricetarget`);
  }

  public quote(symbol: string): Observable<Quote> {
    return this.http.get<Quote>(`api/stocks/${symbol}/quote`);
  }
}
