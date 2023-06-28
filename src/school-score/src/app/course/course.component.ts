import { Component, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatTable } from '@angular/material/table';

import { RegisteredOpenSubject } from '../models/paging';
import { ClassroomStudentService } from 'src/app/services/classroom-student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent {

  data_registerOpenSubject = new RegisteredOpenSubject;
  displayData: any = [];

  @ViewChild(MatTable) table!: MatTable<any>;

  constructor(
    private classroomStudentService: ClassroomStudentService,
    private classroomTierService: ClassroomTierService,
  ) { }

  ngOnInit(): void {
    this.getServerData(undefined);
  }

  async getServerData(event?: PageEvent) {
    this.data_registerOpenSubject = await this.classroomStudentService.GetRegisterOpenSubjects(this.data_registerOpenSubject.SearchText, event);

    this.data_registerOpenSubject.Data.forEach((cos: any) => {
      this.displayData.push({
        Classroom: `${this.GetTierAbb(cos.Classroom.Tier)} ${cos.Classroom.ClassYear} / ${cos.Classroom.Subclass}`,
      });
      cos.OpenSubjectIds.forEach((osId: any) => {
        let openSubject = this.data_registerOpenSubject.OpenSubjects.filter((x: any) => x.Id == osId)[0];
        let subject = this.data_registerOpenSubject.Subjects.filter((x: any) => x.Id == openSubject.SubjectId)[0];
        this.displayData.push({
          Subject: `${subject.Code} ${subject.Name}`,
          ClassroomId: cos.Classroom.Id,
          OpenSubjectId: openSubject.Id,
        });
      });
    });
    this.table.renderRows();

    return event;
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

  GetTierAbb(tier: any) {
    return this.classroomTierService.GetAbb(tier);
  }

  fetchSubject(subjectIds: any): any {
    return this.data_registerOpenSubject.Subjects.filter((x: any) => subjectIds.indexOf(x.Id) >= 0);
  }

}
