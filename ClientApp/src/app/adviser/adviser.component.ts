import {Component, OnInit} from '@angular/core';
import {Title} from '@angular/platform-browser';

@Component({
  selector: 'app-adviser',
  templateUrl: './adviser.component.html'
})
export class AdviserComponent implements OnInit {

  public constructor(private title: Title) {
  }

  public ngOnInit(): void {
    this.title.setTitle('Adviser · InvestingOak');
  }
}
