import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.development';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  
  dataUrl = environment.API_URL + "/api/accounts";

  fg: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private snackBar: MatSnackBar) {
    this.fg = this.fb.group({
      "Username": [null, Validators.required],
      "Password": [null, Validators.required],
    });
  }

  ngOnInit(): void {
  }

  onSave() {
    if (this.fg.valid) {
      this.http.post<any>(this.dataUrl, this.fg.value).subscribe({
        next: (response) => {
          localStorage.setItem("token", response.Token);
          this.router.navigate(['/home']);
          this.snackBar.open('Login success.', "Close", { duration: 5000 });
        },
        error: (error) => {
          this.snackBar.open('Can not login.', "Close", { duration: 5000 });
          console.log(error);
        },
      });
    }
  }
}
