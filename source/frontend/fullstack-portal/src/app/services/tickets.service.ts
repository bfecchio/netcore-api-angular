import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedList } from '../models/paginated-list';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class TicketsService {

  constructor(
    private httpClient: HttpClient
  ) { }

  public findAll(
    { pageIndex, pageSize, airlineId, originId, destinationId, scheduled }: { pageIndex: number; pageSize: number; airlineId?: string; originId?: string; destinationId?: string; scheduled?: string; }
  ): Observable<PaginatedList> {

    let params = new HttpParams()
      .set('pageIndex', (pageIndex + 1).toString())
      .set('pageSize',  (pageSize ?? 5).toString());

    if (airlineId) {
      params = params.append('airlineId', airlineId);
    }

    if (originId) {
      params = params.append('originId', originId);
    }

    if (destinationId) {
      params = params.append('destinationId', destinationId);
    }

    if (scheduled) {
      params = params.append('scheduled', scheduled);
    }

    return this.httpClient.get<PaginatedList>(`${environment.urlServices}/api/v1/tickets`, { params });
  }
}
