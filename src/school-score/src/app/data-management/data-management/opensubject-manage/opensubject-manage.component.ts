import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel, RegisteredOpenSubject } from '../../../models/paging';
import { ClassroomStudentService } from 'src/app/services/classroom-student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';
import { OpenSubjectService } from 'src/app/services/open-subject.service';

@Component({
  selector: 'app-opensubject-manage',
  templateUrl: './opensubject-manage.component.html',
  styleUrls: ['./opensubject-manage.component.css']
})
export class OpensubjectManageComponent {

  data_openSubject = new PagingModel;
  data_registerOpenSubject = new RegisteredOpenSubject;

  constructor(
    private classroomStudentService: ClassroomStudentService,
    private openSubjectService: OpenSubjectService,
    private classroomTierService: ClassroomTierService,
  ) { }

  ngOnInit(): void {
    this.getOpenSubjectData(undefined);
    this.getRegisterOpenSubjectData(undefined);
  }

  async getOpenSubjectData(event?: PageEvent) {
    this.data_openSubject = await this.openSubjectService.Gets(this.data_openSubject.SearchText, event);
    return event;
  }

  async getRegisterOpenSubjectData(event?: PageEvent) {
    this.data_registerOpenSubject = await this.classroomStudentService.GetRegisterOpenSubjects(this.data_registerOpenSubject.SearchText, event);
    return event;
  }

  GetTierAbb(tier: any) {
    return this.classroomTierService.GetAbb(tier);
  }

  fetchSubject(openSubjectIds: any): any {
    let subjectIds = this.data_registerOpenSubject.OpenSubjects.filter((x: any) => openSubjectIds.indexOf(x.Id) >= 0).map((x: any) => x.SubjectId);
    return this.data_registerOpenSubject.Subjects.filter((x: any) => subjectIds.indexOf(x.Id) >= 0);
  }

}
