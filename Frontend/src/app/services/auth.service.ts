import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private jwtHelper = new JwtHelperService(); // jwt helper
  private apiUrl = environment.apiUrl; // api url

  constructor(private http: HttpClient) {}

  register(user: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/register`, user);
  }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/login`, credentials);
  }

  getRole(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    }
    return null;
  }

  isLogged(): boolean {
    const token = localStorage.getItem('token'); // check jwt token
    return token !== null && !this.jwtHelper.isTokenExpired(token);
  }

  isAdmin(): boolean {
    const roles = this.getRole(); 
    return roles ? roles.includes('admin') : false;
  }

  logout(): void {
    localStorage.removeItem('token');
  }
}
