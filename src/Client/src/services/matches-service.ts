import { Injectable } from '@angular/core';
import { PaginationFilter } from '../models/queries/paginationFilter';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/views/paginated-result';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MemberModel } from '../models/views/member-model';

@Injectable({
  providedIn: 'root',
})
export class MatchesService {
  constructor(private httpClient: HttpClient) { }
  private URI: string = 'https://localhost:7111/api/members/matches';
  
  public getMatches(paginationFilter: PaginationFilter): Observable<PaginatedResult<MemberModel>> {
    let params = new HttpParams();
    params = params.append('pageNumber', paginationFilter.pageNumber);
    params = params.append('pageSize', paginationFilter.pageSize);
    return this.httpClient.get<PaginatedResult<MemberModel>>(this.URI, { params });
  }
}
