import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';

import { MatSnackBar } from '@angular/material/snack-bar';

import { API_URL } from '../config';
import { AuthService } from './auth.service';
import { BaseService } from './base.service';
import { PagingModel } from '../models/paging';

@Injectable({
  providedIn: 'root'
})
export class BaseApiService extends BaseService {

  controllername: any;

  constructor(http: HttpClient, router: Router, snackBar: MatSnackBar, authService: AuthService) {
    super(http, router, snackBar, authService);
  }

  SetControllerName(controllername: any) {
    this.controllername = controllername;
  }

  Gets(searchText: any, event?: PageEvent): Promise<PagingModel> {
    let url = `${API_URL}/api/${this.controllername}?search=${searchText ?? ""}&page=${(event?.pageIndex ?? this.firstPage) + 1}&pagesize=${event?.pageSize ?? this.defaultPageSize}`;
    return this.GetDataPagging(url, event);
  }

  GetAll(searchText: any): Promise<PagingModel> {
    let url = `${API_URL}/api/${this.controllername}?search=${searchText ?? ""}&page=0`;
    return this.GetDataPagging(url);
  }

  Create(request: any): Promise<any> {
    let url = `${API_URL}/api/${this.controllername}`;
    return this.Post(url, request);
  }

  Get(id: any): Promise<any> {
    let url = `${API_URL}/api/${this.controllername}/${id}`;
    return this.GetData(url);
  }

  Replace(id: any, request: any): Promise<any> {
    let url = `${API_URL}/api/${this.controllername}/${id}`;
    return this.Put(url, request);
  }

  Delete(id: any): Promise<any> {
    let url = `${API_URL}/api/${this.controllername}/delete/${id}`;
    return this.DeleteData(url);
  }
}
