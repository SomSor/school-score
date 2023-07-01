import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel } from '../../../models/paging';
import { TeacherService } from 'src/app/services/teacher.service';
import { StudentService } from 'src/app/services/student.service';

@Component({
  selector: 'app-person-manage',
  templateUrl: './person-manage.component.html',
  styleUrls: ['./person-manage.component.css']
})
export class PersonManageComponent {

  data_teacher = new PagingModel;
  data_student = new PagingModel;

  constructor(
    private teacherService: TeacherService,
    private studentService: StudentService,
  ) { }

  ngOnInit(): void {
    this.getTeacherData(undefined);
    this.getStudentData(undefined);
  }

  async getTeacherData(event?: PageEvent) {
    this.data_teacher = await this.teacherService.Gets(this.data_teacher.SearchText, event);
    return event;
  }

  async getStudentData(event?: PageEvent) {
    this.data_student = await this.studentService.Gets(this.data_student.SearchText, event);
    return event;
  }

}
