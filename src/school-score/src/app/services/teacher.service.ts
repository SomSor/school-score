import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { MatSnackBar } from '@angular/material/snack-bar';

import { AuthService } from './auth.service';
import { BaseApiService } from './base-api.service';


@Injectable({
  providedIn: 'root'
})
export class TeacherService extends BaseApiService {

  constructor(http: HttpClient, router: Router, snackBar: MatSnackBar, authService: AuthService) {
    super(http, router, snackBar, authService);
    this.SetControllerName('teachers');
  }

}
