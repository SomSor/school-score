import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { MatSnackBar } from '@angular/material/snack-bar';

import { AuthService } from './auth.service';
import { BaseApiService } from './base-api.service';
import { PageEvent } from '@angular/material/paginator';
import { SchoolYearPaging } from '../models/paging';

@Injectable({
  providedIn: 'root'
})
export class SchoolYearService extends BaseApiService {

  constructor(http: HttpClient, router: Router, snackBar: MatSnackBar, authService: AuthService) {
    super(http, router, snackBar, authService);
    this.SetControllerName('schoolyears');
  }

  GetsWithCurrent(searchText: any, event?: PageEvent): Promise<SchoolYearPaging> {
    let url = `${this.apiUrl}/api/${this.controllername}?search=${searchText ?? ""}&page=${(event?.pageIndex ?? this.firstPage) + 1}&pagesize=${event?.pageSize ?? this.defaultPageSize}`;
    return this.GetDataPagging(url, event);
  }

  SetCurrent(id: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/${id}/current`;
    return this.Put(url, {});
  }

}
