import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';

import { MatSnackBar } from '@angular/material/snack-bar';

import { environment } from '../../environments/environment.development';
import { AuthService } from './auth.service';
import { BaseService } from './base.service';
import { PagingModel } from '../models/paging';

@Injectable({
  providedIn: 'root'
})
export class BaseApiService extends BaseService {

  controllername: any;
  apiUrl: any;

  constructor(http: HttpClient, router: Router, snackBar: MatSnackBar, authService: AuthService) {
    super(http, router, snackBar, authService);
    this.apiUrl = environment.API_URL;
  }

  SetControllerName(controllername: any) {
    this.controllername = controllername;
  }

  Gets(searchText: any, event?: PageEvent): Promise<PagingModel> {
    let url = `${this.apiUrl}/api/${this.controllername}?search=${searchText ?? ""}&page=${(event?.pageIndex ?? this.firstPage) + 1}&pagesize=${event?.pageSize ?? this.defaultPageSize}`;
    return this.GetDataPagging(url, event);
  }

  GetAll(searchText: any): Promise<PagingModel> {
    let url = `${this.apiUrl}/api/${this.controllername}?search=${searchText ?? ""}&page=0`;
    return this.GetDataPagging(url);
  }

  Create(request: any, showMessage?: boolean, message?: any, callback?: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}`;
    return this.Post(url, request, showMessage, message, callback);
  }

  Get(id: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/${id}`;
    return this.GetData(url);
  }

  Replace(id: any, request: any, showMessage?: boolean, message?: any, callback?: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/${id}`;
    return this.Put(url, request, showMessage, message, callback);
  }

  Delete(id: any): Promise<any> {
    let url = `${this.apiUrl}/api/${this.controllername}/delete/${id}`;
    return this.DeleteData(url);
  }
}
