import { HttpClient, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { MemberModel } from "../models/views/member-model";
import { Injectable, signal } from "@angular/core";
import { MemberMatchView } from "../models/views/member-match-view";
import { ToasterService } from "./general/toaster-service";
import { ToastView } from "../models/views/toast-view";
import { Severity } from "../models/enums/severity";

@Injectable({
    providedIn: 'root'
})
export class MembersService {
    private members = signal<Array<MemberModel> | null>(null);
    public readonly _members = this.members.asReadonly();
    private URI: string = 'http://localhost:5138/api/members';
    private LikeUri: string = '/likeOrMatch/';
    constructor(private httpClient: HttpClient, private toasterService: ToasterService) { }
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
                    let toast = new ToastView();
                    toast.title = 'Congrats!';
                    toast.description = 'You have a new match!';
                    toast.severity = Severity.Success;
                    this.toasterService.addToast(toast);
                    this.loadMembers();
                }
                else{
                    this.loadMembers();
                }
            }
        })
    }
}