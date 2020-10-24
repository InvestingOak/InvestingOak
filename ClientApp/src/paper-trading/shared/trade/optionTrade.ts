import {OptionLeg} from './optionLeg';
import {Position} from './position';

export class OptionTrade extends Position {
  public legs: OptionLeg[];
}
