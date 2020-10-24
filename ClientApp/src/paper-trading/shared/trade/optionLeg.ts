import {OptionType} from './optionType';

export class OptionLeg {
  public id: number;
  public quantity: number;
  public expiration: Date;
  public strike: number;
  public type: OptionType;
  public openPrice: number;
  public closePrice: number;
}
