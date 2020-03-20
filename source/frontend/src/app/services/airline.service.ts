import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AirlineService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public listAll(): Observable<any> {
    return this.httpClient.get(`${environment.urlServices}/api/v1/airlines`);
  }
}
