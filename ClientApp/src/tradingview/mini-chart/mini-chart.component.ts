import {AfterViewInit, Component, ElementRef, Inject, Input} from '@angular/core';
import {DOCUMENT} from '@angular/common';

@Component({
  selector: 'app-mini-chart',
  templateUrl: './mini-chart.component.html'
})
export class MiniChartComponent implements AfterViewInit {

  @Input()
  public symbol: string;

  public constructor(@Inject(DOCUMENT) private document: Document, private elementRef: ElementRef) { }

  public ngAfterViewInit(): void {
    const params = {
      symbol: this.symbol,
      width: '100%',
      height: '100%',
      locale: 'en',
      dateRange: '1D',
      colorTheme: 'light',
      trendLineColor: 'rgba(106, 168, 79, 1)',
      underLineColor: 'rgba(182, 215, 168, 1)',
      isTransparent: false,
      autosize: true,
      largeChartUrl: ''
    };

    const script = this.document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://s3.tradingview.com/external-embedding/embed-widget-mini-symbol-overview.js';
    script.async = true;
    script.text = JSON.stringify(params);
    this.elementRef.nativeElement.appendChild(script);
  }
}
