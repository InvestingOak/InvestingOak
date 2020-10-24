import {Component} from '@angular/core';
import {ProjectService} from '../../shared/project.service';
import {Project} from '../../shared/project';

@Component({
  selector: 'app-project-summary',
  templateUrl: './project-summary.component.html'
})
export class ProjectSummaryComponent {

  public get project(): Project {
    return this.projectService.project;
  }

  public constructor(private projectService: ProjectService) {
  }
}
