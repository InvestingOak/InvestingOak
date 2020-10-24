import {Position} from './trade/position';

export class Project {
  public name?: string;
  public description?: string;
  public creationDate?: Date;
  public initialBalance?: number;
  public balance?: number;
  public positions?: Position[];
}
