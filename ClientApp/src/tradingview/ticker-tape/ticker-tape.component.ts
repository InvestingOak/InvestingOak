import {AfterViewInit, Component, ElementRef, Inject} from '@angular/core';
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'tradingview-ticker-tape',
  templateUrl: './ticker-tape.component.html'
})
export class TickerTapeComponent implements AfterViewInit {

  constructor(@Inject(DOCUMENT) private document: Document, private elementRef: ElementRef) { }

  public ngAfterViewInit() {
    const params = {
      "symbols":[
        {
          "proName": "FOREXCOM:SPXUSD",
          "title": "S&P 500"
        },
        {
          "proName": "FOREXCOM:NSXUSD",
        "title": "Nasdaq 100"
        },
        {
          "proName": "FX_IDC:EURUSD",
          "title": "EUR/USD"
        },
        {
          "proName": "BITSTAMP:BTCUSD",
          "title": "BTC/USD"
        }
      ],
      "colorTheme": "light",
      "isTransparent": true,
      "displayMode": "regular",
      "locale": "en"
    };

    const script = this.document.createElement('script');
    script.type = 'text/javascript';
    script.src = 'https://s3.tradingview.com/external-embedding/embed-widget-ticker-tape.js';
    script.async = true;
    script.text = JSON.stringify(params);
    this.elementRef.nativeElement.appendChild(script);
  }
}
