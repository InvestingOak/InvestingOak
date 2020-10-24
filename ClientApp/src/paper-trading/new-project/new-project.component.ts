import {Component} from '@angular/core';
import {ProjectService} from '../shared/project.service';
import {Router} from '@angular/router';
import {ProjectCreateModel} from '../shared/projectCreateModel';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html'
})
export class NewProjectComponent {

  public readonly initialBalance: number = 100_000.00;
  public errorMessage = '';

  public project: ProjectCreateModel = {
    name: '',
    description: '',
    initialBalance: this.initialBalance
  };

  public constructor(private projectService: ProjectService, private router: Router) {
  }

  public onSubmit(): void {
    this.projectService.createProject(this.project).subscribe(
      () => {
        this.projectService.getProjectList();
        this.router.navigate([`/paper/project/${this.project.name}`]).then();
      },
      err => {
        this.errorMessage = `Failed to create new project: ${err}`;
      }
    );
  }
}
