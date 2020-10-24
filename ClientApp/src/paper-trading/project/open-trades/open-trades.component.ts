import {Component} from '@angular/core';
import {ProjectService} from '../../shared/project.service';
import {Project} from '../../shared/project';

@Component({
  selector: 'app-open-trades',
  templateUrl: './open-trades.component.html'
})
export class OpenTradesComponent {

  public get project(): Project {
    return this.projectService.project;
  }

  public constructor(private projectService: ProjectService) {
  }
}
