import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CompanyProfile2, News, NewsSentiment, PriceTarget, RecommendationTrend, StockSymbol} from './responses';
import {IdType} from './idType';

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
  public companyProfile2(idType: IdType, value: string): Observable<CompanyProfile2> {
    return this.http.get<CompanyProfile2>(`${this.baseUrl}/stock/profile2?${this.idTypeToString(idType)}=${value}&token=${this.apiKey}`);
  }

  /**
   * List supported stocks.
   * https://finnhub.io/docs/api#stock-symbols
   * @param exchange Exchange you want to get the list of symbols from.
   */
  public symbols(exchange: string): Observable<StockSymbol[]> {
    return this.http.get<StockSymbol[]>(`${this.baseUrl}/stock/symbol?exchange=${exchange}&token=${this.apiKey}`);
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

  /**
   * Get company's news sentiment and statistics. This endpoint is only available for US companies.
   * https://finnhub.io/docs/api#news-sentiment
   * @param symbol Company symbol.
   */
  public newsSentiment(symbol: string): Observable<NewsSentiment> {
    return this.http.get<NewsSentiment>(`${this.baseUrl}/news-sentiment?symbol=${symbol}&token=${this.apiKey}`);
  }

  /**
   * Get company peers. Return a list of peers in the same country and GICS sub-industry
   * https://finnhub.io/docs/api#company-peers
   * @param symbol Company symbol.
   */
  public peers(symbol: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/stock/peers?symbol${symbol}&token=${this.apiKey}`);
  }

  /**
   * Get latest analyst recommendation trends for a company.
   * https://finnhub.io/docs/api#recommendation-trends
   * @param symbol Company symbol.
   */
  public recommendationTrends(symbol: string): Observable<RecommendationTrend[]> {
    return this.http.get<RecommendationTrend[]>(`${this.baseUrl}/stock/recommendation?symbol=${symbol}&token=${this.apiKey}`);
  }

  /**
   * Get latest price target consensus.
   * https://finnhub.io/docs/api#price-target
   * @param symbol Company symbol.
   */
  public priceTarget(symbol: string): Observable<PriceTarget> {
    return this.http.get<PriceTarget>(`${this.baseUrl}/stock/price-target?symbol=${symbol}&token=${this.apiKey}`);
  }

  private idTypeToString(idType: IdType): string {
    switch (idType) {
      case IdType.Symbol:
        return 'symbol';
      case IdType.Isin:
        return 'isin';
      case IdType.Cusip:
        return 'cusip';
    }
  }
}
