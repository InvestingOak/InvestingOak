import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthorizeService} from '../api-authorization/authorize.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public constructor(private http: HttpClient, private authService: AuthorizeService) {
  }

  public async getUser(): Promise<void> {
    const accessToken = await this.authService.getAccessToken().toPromise();
    const authHeader = {Authorization: `Bearer ${accessToken}`};
    this.http.get('api/account/get', {headers: authHeader}).toPromise().then();
  }
}
