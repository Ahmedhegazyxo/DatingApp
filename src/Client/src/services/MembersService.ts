import { HttpClient, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { MemberModel } from "../models/views/MemberModel";
import { Injectable, signal } from "@angular/core";
import { MemberMatchView } from "../models/views/MemberMatchView";

@Injectable({
    providedIn: 'root'
})
export class MembersService {
    private members = signal<Array<MemberModel> | null>(null);
    public readonly _members = this.members.asReadonly();
    private URI: string = 'http://localhost:5138/api/members';
    private LikeUri: string = '/likeOrMatch/';
    constructor(private httpClient: HttpClient) { }
    public loadMembers(): void {
        this.httpClient.get<Array<MemberModel>>(this.URI, { observe: 'response' }).subscribe({
            next: (res: HttpResponse<Array<MemberModel>>) => this.onAcceptedCallback(res)
        })
    }
    private onAcceptedCallback(response: HttpResponse<Array<MemberModel>>): void {
        this.members.set(response.body);
    }
    public LikeMember(id: string) {
        this.httpClient.post<MemberMatchView>(this.URI + this.LikeUri + id, id).subscribe({
            next: (e) => {
                if (e.isMatched){
                    
                    this.loadMembers();
                }
                else{
                    this.loadMembers();
                }
            }
        })
    }
}