import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CompanyProfile2, News, NewsSentiment, PriceTarget, Quote, RecommendationTrend, StockCandles, StockSymbol} from './responses';
import {IdType} from './idType';
import {shareReplay} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FinnhubService {

  private readonly baseUrl = 'https://finnhub.io/api/v1';
  private readonly apiKey = 'btt5puv48v6q0kg1m610';

  public constructor(private http: HttpClient) {
  }

  private static idTypeToString(idType: IdType): string {
    switch (idType) {
      case IdType.Symbol:
        return 'symbol';
      case IdType.Isin:
        return 'isin';
      case IdType.Cusip:
        return 'cusip';
    }
  }

  private static dateToISOString(date: Date): string {
    return date.toISOString().split('T')[0].toString();
  }

  /**
   * Get general information of a company. You can query by symbol, ISIN or CUSIP. This is the free version of Company Profile.
   * https://finnhub.io/docs/api#company-profile2
   * @param idType
   * @param value
   */
  public companyProfile2(idType: IdType, value: string): Observable<CompanyProfile2> {
    return this.http.get<CompanyProfile2>(`${this.baseUrl}/stock/profile2?${FinnhubService.idTypeToString(idType)}=${value}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * List supported stocks.
   * https://finnhub.io/docs/api#stock-symbols
   * @param exchange Exchange you want to get the list of symbols from.
   */
  public symbols(exchange: string): Observable<StockSymbol[]> {
    return this.http.get<StockSymbol[]>(`${this.baseUrl}/stock/symbol?exchange=${exchange}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * Get latest market news.
   * https://finnhub.io/docs/api#market-news
   * @param category This parameter can be 1 of the following values: general, forex, crypto, merger.
   * @param minId Use this field to get only news after this ID. Default to 0
   */
  public marketNews(category: 'general' | 'forex' | 'crypto' | 'merger', minId = 0): Observable<News[]> {
    return this.http.get<News[]>(`${this.baseUrl}/news?category=${category}&minId=${minId}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * List latest company news by symbol. This endpoint is only available for North American companies.
   * https://finnhub.io/docs/api#company-news
   * @param symbol Company symbol.
   * @param from From date YYYY-MM-DD.
   * @param to To date YYYY-MM-DD.
   */
  public companyNews(symbol: string, from: Date, to: Date): Observable<News[]> {
    const fromStr = FinnhubService.dateToISOString(from);
    const toStr = FinnhubService.dateToISOString(to);
    return this.http.get<News[]>(`${this.baseUrl}/company-news?symbol=${symbol}&from=${fromStr}&to=${toStr}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * Get company's news sentiment and statistics. This endpoint is only available for US companies.
   * https://finnhub.io/docs/api#news-sentiment
   * @param symbol Company symbol.
   */
  public newsSentiment(symbol: string): Observable<NewsSentiment> {
    return this.http.get<NewsSentiment>(`${this.baseUrl}/news-sentiment?symbol=${symbol}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * Get company peers. Return a list of peers in the same country and GICS sub-industry
   * https://finnhub.io/docs/api#company-peers
   * @param symbol Company symbol.
   */
  public peers(symbol: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/stock/peers?symbol=${symbol}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * Get latest analyst recommendation trends for a company.
   * https://finnhub.io/docs/api#recommendation-trends
   * @param symbol Company symbol.
   */
  public recommendationTrends(symbol: string): Observable<RecommendationTrend[]> {
    return this.http.get<RecommendationTrend[]>(`${this.baseUrl}/stock/recommendation?symbol=${symbol}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * Get latest price target consensus.
   * https://finnhub.io/docs/api#price-target
   * @param symbol Company symbol.
   */
  public priceTarget(symbol: string): Observable<PriceTarget> {
    return this.http.get<PriceTarget>(`${this.baseUrl}/stock/price-target?symbol=${symbol}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * Get real-time quote data for US stocks. Constant polling is not recommended.
   * Use websocket if you need real-time update.
   * https://finnhub.io/docs/api#quote
   * @param symbol Company symbol.
   */
  public quote(symbol: string): Observable<Quote> {
    return this.http.get<Quote>(`${this.baseUrl}/quote?symbol=${symbol}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  /**
   * Get candlestick data for stocks going back 25 years for US stocks.
   * @param symbol
   * @param resolution
   * @param from
   * @param to
   */
  public stockCandles(symbol: string, resolution: '1' | '5' | '15' | '30' | '60' | 'D' | 'W' | 'M',
                      from: Date, to: Date): Observable<StockCandles> {
    const fromStr = from.getTime() / 1000 | 0;
    const toStr = to.getTime() / 1000 | 0;
    console.log(`${this.baseUrl}/stock/candle?symbol=${symbol}&resolution=${resolution}&from=${fromStr}&to=${toStr}&token=${this.apiKey}`);
    return this.http.get<StockCandles>(`${this.baseUrl}/stock/candle?symbol=${symbol}&resolution=${resolution}&from=${fromStr}&to=${toStr}&token=${this.apiKey}`)
      .pipe(shareReplay({bufferSize: 1, refCount: true}));
  }

  public getExchangeFromResponse(exchange: string): string {
    switch (exchange) {
      case 'NASDAQ NMS - GLOBAL MARKET':
        return 'NASDAQ';
      case 'NEW YORK STOCK EXCHANGE, INC.':
        return 'NYSE';
      case 'NYSE MKT LLC':
        return 'AMEX';
      case 'OTC MARKETS':
        return 'OTC';
      default:
        return exchange;
    }
  }
}
