import {Position} from './position';

export class StockTrade extends Position {
  public quantity: number;
  public openPrice: number;
  public closePrice: number;
}
