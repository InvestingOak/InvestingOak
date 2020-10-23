import {AfterViewInit, Component, ElementRef, Inject, Input} from '@angular/core';
import {DOCUMENT} from '@angular/common';

@Component({
  selector: 'app-symbol-overview',
  templateUrl: './symbol-overview.component.html'
})
export class SymbolOverviewComponent implements AfterViewInit {

  @Input()
  public exchange: string;

  @Input()
  public symbol: string;

  @Input()
  public name: string;

  @Input()
  public period: string;

  @Input()
  public red: boolean;

  public constructor(@Inject(DOCUMENT) private document: Document, private elementRef: ElementRef) {
  }

  public ngAfterViewInit(): void {

    const params = {
      symbols: [[`this.name`, `${this.exchange}:${this.symbol}|${this.period}`]],
      chartOnly: true,
      width: '100%',
      height: '100%',
      locale: 'en',
      colorTheme: 'light',
      gridLineColor: 'rgba(0, 0, 0, 0.25)',
      trendLineColor: 'rgba(106, 168, 79, 1)',
      fontColor: '#787b86',
      underLineColor: 'rgba(182, 215, 168, 1)',
      isTransparent: true,
      autosize: true,
      container_id: 'tradingview_12b13'
    };

    const script = this.document.createElement('script');
    script.type = 'text/javascript';
    script.text = `new TradingView.MediumWidget(${JSON.stringify(params)});`;
    this.elementRef.nativeElement.appendChild(script);
  }
}
