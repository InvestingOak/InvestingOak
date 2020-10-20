import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Title} from '@angular/platform-browser';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html'
})
export class NotFoundComponent implements OnInit {

  public constructor(private router: Router, private title: Title) {
  }

  public goHome(): void {
    this.router.navigate(['/']).then();
  }

  public ngOnInit(): void {
    this.title.setTitle('Page not found · InvestingOak');
  }
}
