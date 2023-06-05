import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { PagingModel } from '../models/paging';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  firstPage = 0;
  defaultPageSize = 50;

  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar, private authService: AuthService) { }

  GetData(url: any, showMessage?: boolean, message?: any): Promise<any> {
    return new Promise(resolve => {
      this.http.get<any>(url, { headers: this.authService.AuthHeaders() }).subscribe({
        next: (response) => resolve(response),
        error: (error) => {
          this.Error(error, showMessage, message);
          resolve(null);
        },
      });
    });
  }

  GetDataPagging(url: any, event?: PageEvent): Promise<any> {
    return new Promise(resolve => {
      this.http.get<any>(url, { headers: this.authService.AuthHeaders() }).subscribe({
        next: (response) => resolve(this.ToPagging(response, event)),
        error: (error) => {
          this.Error(error);
          resolve(null);
        },
      });
    });
  }

  Post(url: any, request: any): Promise<any> {
    return new Promise(resolve => {
      this.http.post(url, request, { headers: this.authService.AuthHeaders() }).subscribe({
        next: (response) => {
          this.snackBar.open("Data created", "Close", { duration: 5000 });
          resolve(response);
        },
        error: (error) => {
          this.Error(error);
          resolve(null);
        },
      })
    });
  }

  Put(url: any, request: any): Promise<any> {
    return new Promise(resolve => {
      this.http.put(url, request, { headers: this.authService.AuthHeaders() }).subscribe({
        next: (response) => {
          this.snackBar.open("Data updated", "Close", { duration: 5000 });
          resolve(response);
        },
        error: (error) => {
          this.Error(error);
          resolve(null);
        },
      })
    });
  }

  PutBlob(url: any, request: any): Promise<any> {
    return new Promise(resolve => {
      this.http.put(url, request, { headers: this.authService.AuthHeaders(), responseType: 'blob' }).subscribe({
        next: (response) => {
          this.snackBar.open("Data updated", "Close", { duration: 5000 });
          resolve(response);
        },
        error: (error) => {
          this.Error(error);
          resolve(null);
        },
      })
    });
  }

  DeleteData(url: any): Promise<any> {
    return new Promise(resolve => {
      this.http.delete(url, { headers: this.authService.AuthHeaders() }).subscribe({
        next: (response) => {
          this.snackBar.open("Data deleted", "Close", { duration: 5000 });
          resolve(response);
        },
        error: (error) => {
          this.Error(error);
          resolve(null);
        },
      })
    });
  }

  ToPagging(data: PagingModel, event?: PageEvent): PagingModel {
    data.SearchText ?? "";
    data.PageIndex = event?.pageIndex ?? 0;
    data.PageSize = event?.pageSize ?? 50;
    data.LastCount = data.PageSize * data.PageIndex;

    return data;
  }

  Error(error: any, showMessage?: boolean, message?: any) {
    if (error.status == 401) {
      this.snackBar.open('Please login', "Close", { duration: 5000 });
      this.router.navigate(['/login']);
    } else if (showMessage) {
      this.snackBar.open(message ?? error.error, "Close", { duration: 5000 });
    }
  }

}
