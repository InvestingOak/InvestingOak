import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, Subject} from 'rxjs';
import {ProjectDescModel} from './projectDescModel';
import {Project} from './project';
import {ProjectCreateModel} from './projectCreateModel';
import {PatchDocument} from './patchDocument';

@Injectable()
export class ProjectService {

  private projectListSubject = new Subject<ProjectDescModel[]>();
  public projectListUpdated$ = this.projectListSubject.asObservable();
  public project = {} as Project;

  public constructor(private http: HttpClient) {
  }

  public getProject(name: string): Observable<Project> {
    return this.http.get<Project>(`/api/projects/${name}`);
  }

  public getProjectList(): void {
    this.http.get<ProjectDescModel[]>('/api/projects')
      .subscribe(list => this.projectListSubject.next(list));
  }

  public createProject(model: ProjectCreateModel): Observable<any> {
    return this.http.post('/api/projects/create', model);
  }

  public updateProject(name: string, patchDocuments: PatchDocument[]): Observable<any> {
    return this.http.patch(`/api/projects/${name}`, patchDocuments);
  }

  public deleteProject(name: string): Observable<any> {
    return this.http.delete(`/api/projects/${name}`);
  }
}
