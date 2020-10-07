import {Component, OnInit} from '@angular/core';
import {AuthorizeService} from '../../api-authorization/authorize.service';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss']
})
export class UserMenuComponent implements OnInit {

  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;

  public constructor(public authorizeService: AuthorizeService) { }

  public ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser()
      .pipe(
        map(u => u && u.name)
      );
  }
}
