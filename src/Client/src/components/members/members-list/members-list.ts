import { AfterViewInit, Component, input, signal, ViewChild, viewChild } from '@angular/core';
import { MemberCard } from '../member-card/member-card';
import { MembersService } from '../../../services/members-service';
import { MemberModel } from '../../../models/views/member-model';
import { PaginatedList } from "../../general/paginated-list/paginated-list";
import { PaginationFilter } from '../../../models/queries/paginationFilter';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../../../models/views/paginated-result';
import { ToastView } from '../../../models/views/toast-view';
import { Severity } from '../../../models/enums/severity';
import { ToasterService } from '../../../services/general/toaster-service';

@Component({
  selector: 'app-members-list',
  imports: [MemberCard, PaginatedList],
  templateUrl: './members-list.html',
  styleUrl: './members-list.css',
})
export class MembersList {
  @ViewChild('paginatedList') paginatedList!: PaginatedList<any>
  constructor(public _membersService: MembersService,
    public toasterService: ToasterService
  ) { }

  protected getMembers = (filter: PaginationFilter): Observable<PaginatedResult<MemberModel>> => {
    return this._membersService.getMembers(filter);
  }
  protected likeUser(memberModel: MemberModel) {
    this._membersService.LikeMember(memberModel.id).subscribe({
      next: (e) => {
        if (e.isMatched) {
          let toast = new ToastView();
          toast.title = 'Congrats!';
          toast.description = 'You have a new match!';
          toast.severity = Severity.Success;
          this.toasterService.addToast(toast);
        }
        else {
        }
        this.paginatedList.refresh();
      }
    })

  }
}
