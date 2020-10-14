import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CompanyProfile2, News} from './responses';

@Injectable({
  providedIn: 'root'
})
export class FinnhubService {

  private readonly baseUrl = 'https://finnhub.io/api/v1'
  private readonly apiKey = 'btt5puv48v6q0kg1m610';

  public constructor(private http: HttpClient) { }

  /**
   * Get general information of a company. You can query by symbol, ISIN or CUSIP. This is the free version of Company Profile.
   * https://finnhub.io/docs/api#company-profile2
   * @param idType
   * @param value
   */
  public companyProfile2(idType: 'symbol' | 'isin' | 'cusip' , value: string): Observable<CompanyProfile2> {
    return this.http.get<CompanyProfile2>(`${this.baseUrl}/stock/profile2?${idType}=${value}&token=${this.apiKey}`);
  }

  /**
   * Get latest market news.
   * https://finnhub.io/docs/api#market-news
   * @param category This parameter can be 1 of the following values: general, forex, crypto, merger.
   * @param minId Use this field to get only news after this ID. Default to 0
   */
  public marketNews(category: 'general' | 'forex' | 'crypto' | 'merger', minId = 0): Observable<News[]> {
    return this.http.get<News[]>(`${this.baseUrl}/news?category=${category}&minId=${minId}&token=${this.apiKey}`);
  }

  /**
   * List latest company news by symbol. This endpoint is only available for North American companies.
   * https://finnhub.io/docs/api#company-news
   * @param symbol Company symbol.
   * @param from From date YYYY-MM-DD.
   * @param to To date YYYY-MM-DD.
   */
  public companyNews(symbol: string, from: string, to: string): Observable<News[]> {
    return this.http.get<News[]>(`${this.baseUrl}/company-news?symbol=${symbol}&from=${from}&to=${to}&token=${this.apiKey}`);
  }
}
