import { HttpClient, HttpErrorResponse, HttpParams, HttpResponse } from "@angular/common/http";
import { MemberModel } from "../models/views/member-model";
import { Injectable, signal } from "@angular/core";
import { LikeResponseView } from "../models/views/like-response-view";
import { ToasterService } from "./general/toaster-service";
import { ToastView } from "../models/views/toast-view";
import { Severity } from "../models/enums/severity";
import { PaginatedResult } from "../models/views/paginated-result";
import { Observable } from "rxjs";
import { PaginationFilter } from "../models/queries/paginationFilter";

@Injectable({
    providedIn: 'root'
})
export class MembersService {
    private URI: string = 'https://localhost:7111/api/members';
    private LikeUri: string = '/likeOrMatch/';
    constructor(private httpClient: HttpClient, private toasterService: ToasterService) { }
    public loadMembers(): void {
        this.getMembers({ pageNumber: 1, pageSize: 50 }).subscribe({
            next: (res: PaginatedResult<MemberModel>) => this.onAcceptedCallback(res)
        })
    }
    public getMembers(paginationFilter: PaginationFilter): Observable<PaginatedResult<MemberModel>> {
        let params = new HttpParams();
        params = params.append('pageNumber', paginationFilter.pageNumber);
        params = params.append('pageSize', paginationFilter.pageSize);
        return this.httpClient.get<PaginatedResult<MemberModel>>(this.URI, { params });
    }
    private onAcceptedCallback(response: PaginatedResult<MemberModel>): void {
    }
        public LikeMember(id: string): Observable<LikeResponseView> {
        return this.httpClient.post<LikeResponseView>(this.URI + this.LikeUri + id, id);
    }
}