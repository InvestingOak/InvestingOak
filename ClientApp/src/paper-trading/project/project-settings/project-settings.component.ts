import {Component} from '@angular/core';
import {ProjectService} from '../../shared/project.service';
import {Router} from '@angular/router';
import {Project} from '../../shared/project';
import {PatchDocument} from '../../shared/patchDocument';

@Component({
  selector: 'app-project-settings',
  templateUrl: './project-settings.component.html'
})
export class ProjectSettingsComponent {

  public name = '';
  public confirmText = '';
  public errorMessage = '';

  public get project(): Project {
    return this.projectService.project;
  }

  public constructor(private projectService: ProjectService, private router: Router) {
  }

  public renameProject(): void {

    const renamePatch: PatchDocument = {
      op: 'replace',
      path: '/name',
      value: this.name
    };

    this.projectService.updateProject(this.project.name, [renamePatch]).subscribe(
      () => {
        this.project.name = this.name;
        this.projectService.getProjectList();
      },
      err => this.errorMessage = `Failed to rename project: ${err}`
    );
  }

  public deleteProject(): void {
    if (this.confirmText == this.projectService.project.name) {
      this.projectService.deleteProject(this.projectService.project.name).subscribe(
        () => {
          this.projectService.project = undefined;
          this.projectService.getProjectList();
          this.router.navigate([`/paper/deleted`]).then();
        },
        err => (this.errorMessage = `Failed to delete project: ${err}`)
      );
    }
  }
}
