import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel } from '../../models/paging';
import { LearningAreaService } from '../../services/learning-area.service';
import { SubjectService } from '../../services/subject.service';
import { TeacherService } from 'src/app/services/teacher.service';
import { StudentService } from 'src/app/services/student.service';
import { ClassroomService } from 'src/app/services/classroom.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-data-management',
  templateUrl: './data-management.component.html',
  styleUrls: ['./data-management.component.css']
})
export class DataManagementComponent {

  data_teacher = new PagingModel;
  data_student = new PagingModel;
  data_learningArea = new PagingModel;
  data_subject = new PagingModel;
  data_classroom = new PagingModel;

  constructor(
    private teacherService: TeacherService,
    private studentService: StudentService,
    private learningAreaService: LearningAreaService,
    private subjectService: SubjectService,
    private classroomService: ClassroomService,
    private classroomTierService: ClassroomTierService,
  ) { }

  ngOnInit(): void {
    this.getTeacherData(undefined);
    this.getStudentData(undefined);
    this.getLearingAreaData(undefined);
    this.getSubjectData(undefined);
    this.getClassroomData(undefined);
  }

  async getTeacherData(event?: PageEvent) {
    this.data_teacher = await this.teacherService.Gets(this.data_teacher.SearchText, event);
    return event;
  }

  async getStudentData(event?: PageEvent) {
    this.data_student = await this.studentService.Gets(this.data_student.SearchText, event);
    return event;
  }

  async getLearingAreaData(event?: PageEvent) {
    this.data_learningArea = await this.learningAreaService.Gets(this.data_learningArea.SearchText, event);
    return event;
  }

  async getSubjectData(event?: PageEvent) {
    this.data_subject = await this.subjectService.Gets(this.data_subject.SearchText, event);
    return event;
  }

  async getClassroomData(event?: PageEvent) {
    this.data_classroom = await this.classroomService.Gets(this.data_subject.SearchText, event);
    return event;
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

}
