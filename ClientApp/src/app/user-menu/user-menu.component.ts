import {Component, OnInit} from '@angular/core';
import {AuthorizeService, IUser} from '../../api-authorization/authorize.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss']
})
export class UserMenuComponent implements OnInit {

  public isAuthenticated: Observable<boolean>;
  public user: Observable<IUser>;

  public constructor(public authorizeService: AuthorizeService) { }

  public ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.user = this.authorizeService.getUser();
  }
}
