import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';

import { MatSnackBar } from '@angular/material/snack-bar';

import { AuthService } from './auth.service';
import { BaseApiService } from './base-api.service';
import { PagingModel, RegisteredOpenSubject } from '../models/paging';

@Injectable({
  providedIn: 'root'
})
export class ClassroomStudentService extends BaseApiService {

  constructor(http: HttpClient, router: Router, snackBar: MatSnackBar, authService: AuthService) {
    super(http, router, snackBar, authService);
    this.SetControllerName('classroomstudents');
  }

  GetRegisterOpenSubjects(searchText: any, event?: PageEvent): Promise<RegisteredOpenSubject> {
    let url = `${this.apiUrl}/api/${this.controllername}/opensubjects?search=${searchText ?? ""}&page=${(event?.pageIndex ?? this.firstPage) + 1}&pagesize=${event?.pageSize ?? this.defaultPageSize}`;
    return this.GetDataPagging(url, event);
  }

  getClassroomOpenSubjectData(classroomid: any, opensubjectid: any): Promise<RegisteredOpenSubject> {
    let url = `${this.apiUrl}/api/${this.controllername}/classrooms/${classroomid}/opensubjects/${opensubjectid}`;
    return this.GetData(url);
  }

  getClassroomOpenSubjectTimetableData(classroomid: any, opensubjectid: any): Promise<RegisteredOpenSubject> {
    let url = `${this.apiUrl}/api/${this.controllername}/classrooms/${classroomid}/opensubjects/${opensubjectid}/timetables`;
    return this.GetData(url);
  }

  RegisterOpenSubjects(request: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/opensubjects`;
    return this.Post(url, request);
  }

  SaveExam(request: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/exam`;
    return this.Put(url, request);
  }

  SaveEvaluate(request: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/evaluate`;
    return this.Put(url, request);
  }

  SaveAttendances(request: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/attendances`;
    return this.Put(url, request);
  }

}
