import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { catchError, shareReplay, tap } from "rxjs/operators";

import * as moment from 'moment';
import * as jwt_decode from "jwt-decode";

import { environment } from 'src/environments/environment';
import { User } from '../models/user';


@Injectable({ providedIn: 'root' })
export class AuthService {
  private isLoggedSubject: BehaviorSubject<boolean>;
  public isLogged: Observable<boolean>;

  constructor(private http: HttpClient) {
    this.isLoggedSubject = new BehaviorSubject<boolean>(this.isLoggedIn());
    this.isLogged = this.isLoggedSubject.asObservable();
  }

  public login(username: string, password: string): Observable<boolean> {
    const params: URLSearchParams = new URLSearchParams();
    params.set('grant_type', 'password');
    params.set('client_id', environment.clientId);
    params.set('client_secret', environment.clientSecret);
    params.set('scope', '');
    params.set('username', username);
    params.set('password', password);

    return this.sendAuthentication(params);
  }

  public logout(): void {
    sessionStorage.clear()
    this.isLoggedSubject.next(false);
  }

  public getToken(): string {
    return sessionStorage.getItem("id_token");
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());    
  }

  //#region Private Methods

  private sendAuthentication(params: URLSearchParams): Observable<boolean> {
    const options = {
      headers: new HttpHeaders()
        .set("Content-Type", "application/x-www-form-urlencoded")
    }

    return this.http.post<any>(`${environment.urlServices}/connect/token`, params.toString(), options)
      .pipe(        
        catchError(error => throwError(error)),
        shareReplay(),
        tap(res => this.setSession(res))
      );
  }

  private setSession(authResult) {
    const expiresAt = moment().add(authResult.expires_in, 'second');
    sessionStorage.setItem('id_token', authResult.access_token);
    sessionStorage.setItem('refresh_token', authResult.refresh_token);
    sessionStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));

    const userInfo: any = jwt_decode(authResult.access_token);
    const user: User = new User(
      userInfo["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
      userInfo["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      userInfo["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]
    )

    sessionStorage.setItem('user', btoa(JSON.stringify(user)));
    this.isLoggedSubject.next(true);
  }

  private getExpiration() {
    const expiration = sessionStorage.getItem("expires_at");
    const expiresAt = JSON.parse(expiration);

    return moment(expiresAt);
  }

  //#endregion
}
