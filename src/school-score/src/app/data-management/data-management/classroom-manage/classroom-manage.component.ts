import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel } from '../../../models/paging';
import { ClassroomService } from 'src/app/services/classroom.service';
import { ClassroomStudentService } from 'src/app/services/classroom-student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-classroom-manage',
  templateUrl: './classroom-manage.component.html',
  styleUrls: ['./classroom-manage.component.css']
})
export class ClassroomManageComponent {

  data_classroom = new PagingModel;
  data_classroomStudent = new PagingModel;

  constructor(
    private classroomService: ClassroomService,
    private classroomStudentService: ClassroomStudentService,
    private classroomTierService: ClassroomTierService,
  ) { }

  ngOnInit(): void {
    this.getClassroomData(undefined);
    this.getClassroomStudentData(undefined);
  }

  async getClassroomData(event?: PageEvent) {
    this.data_classroom = await this.classroomService.Gets(this.data_classroom.SearchText, event);
    return event;
  }

  async getClassroomStudentData(event?: PageEvent) {
    this.data_classroomStudent = await this.classroomStudentService.Gets(this.data_classroomStudent.SearchText, event);
    return event;
  }

  GetTier(tier: any) {
    return this.classroomTierService.GetAbb(tier);
  }

}
