import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ProjectService} from '../shared/project.service';
import {Project} from '../shared/project';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html'
})
export class ProjectComponent implements OnInit {

  public get project(): Project {
    return this.projectService.project;
  }

  public constructor(private route: ActivatedRoute, private projectService: ProjectService) {
  }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.projectService.getProject(params.id).subscribe(project => {
        this.projectService.project = project;
      });
    });
  }
}
