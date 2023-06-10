import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  public logout() {
    return localStorage.removeItem("token");
  }

  public getToken(): string {
    return localStorage.getItem("token") ?? "";
  }

  public AuthHeaders(): HttpHeaders {
    var token = this.getToken();
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', "Bearer " + token);

    return headers;
  }

  public tokenExpired(): boolean {
    var token = this.getToken();
    if(!token) return true;
    const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    return Math.floor((new Date).getTime() / 1000) >= expiry;
  }

}
