import {Component, OnInit} from '@angular/core';
import {ProjectDescModel} from '../shared/projectDescModel';
import {ProjectService} from '../shared/project.service';
import {faPlus} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-project-tree',
  templateUrl: './project-tree.component.html',
  styleUrls: ['./project-tree.component.scss']
})
export class ProjectTreeComponent implements OnInit {

  public plus = faPlus;

  public projectList: ProjectDescModel[];

  public constructor(public projectService: ProjectService) {
  }

  public ngOnInit(): void {
    this.projectService.projectListUpdated$.subscribe(projectList => this.projectList = projectList);
    this.projectService.getProjectList();
  }
}
