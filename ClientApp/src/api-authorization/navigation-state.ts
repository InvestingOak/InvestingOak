import {ReturnUrlType} from './api-authorization.constants';

export interface NavigationState {
  [ReturnUrlType]: string;
}
