import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel, RegisteredOpenSubject } from '../../models/paging';
import { LearningAreaService } from '../../services/learning-area.service';
import { SubjectService } from '../../services/subject.service';
import { TeacherService } from 'src/app/services/teacher.service';
import { StudentService } from 'src/app/services/student.service';
import { ClassroomService } from 'src/app/services/classroom.service';
import { ClassroomStudentService } from 'src/app/services/classroom-student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';
import { OpenSubjectService } from 'src/app/services/open-subject.service';

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
  data_classroomStudent = new PagingModel;
  data_openSubject = new PagingModel;
  data_registerOpenSubject = new RegisteredOpenSubject;

  constructor(
    private teacherService: TeacherService,
    private studentService: StudentService,
    private learningAreaService: LearningAreaService,
    private subjectService: SubjectService,
    private classroomService: ClassroomService,
    private classroomStudentService: ClassroomStudentService,
    private openSubjectService: OpenSubjectService,
    private classroomTierService: ClassroomTierService,
  ) { }

  ngOnInit(): void {
    this.getTeacherData(undefined);
    this.getStudentData(undefined);
    this.getLearingAreaData(undefined);
    this.getSubjectData(undefined);
    this.getClassroomData(undefined);
    this.getClassroomStudentData(undefined);
    this.getOpenSubjectData(undefined);
    this.getRegisterOpenSubjectData(undefined);
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

  async getClassroomStudentData(event?: PageEvent) {
    this.data_classroomStudent = await this.classroomStudentService.Gets(this.data_subject.SearchText, event);
    return event;
  }

  async getOpenSubjectData(event?: PageEvent) {
    this.data_openSubject = await this.openSubjectService.Gets(this.data_openSubject.SearchText, event);
    return event;
  }

  async getRegisterOpenSubjectData(event?: PageEvent) {
    this.data_registerOpenSubject = await this.classroomStudentService.GetRegisterOpenSubjects(this.data_registerOpenSubject.SearchText, event);
    return event;
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

  GetTierAbb(tier: any) {
    return this.classroomTierService.GetAbb(tier);
  }

  fetchSubject(openSubjectIds: any): any {
    let subjectIds = this.data_registerOpenSubject.OpenSubjects.filter((x: any) => openSubjectIds.indexOf(x.Id) >= 0).map((x: any) => x.SubjectId);
    return this.data_registerOpenSubject.Subjects.filter((x: any) => subjectIds.indexOf(x.Id) >= 0);
  }

}
