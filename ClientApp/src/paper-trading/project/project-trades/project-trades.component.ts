import {Component} from '@angular/core';
import {ProjectService} from '../../shared/project.service';
import {Project} from '../../shared/project';

@Component({
  selector: 'app-project-trades',
  templateUrl: './project-trades.component.html'
})
export class ProjectTradesComponent {

  public get project(): Project {
    return this.projectService.project;
  }

  public constructor(private projectService: ProjectService) {
  }
}
