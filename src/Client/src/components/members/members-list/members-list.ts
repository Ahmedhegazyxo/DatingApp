import { AfterViewInit, Component, input } from '@angular/core';
import { MemberCard } from '../member-card/member-card';
import { MembersService } from '../../../services/members-service';
import { MemberModel } from '../../../models/views/member-model';
import { UserModel } from '../../../models/views/user-model';

@Component({
  selector: 'app-members-list',
  imports: [MemberCard],
  templateUrl: './members-list.html',
  styleUrl: './members-list.css',
})
export class MembersList implements AfterViewInit {
  constructor(public _membersService: MembersService) { }
  ngAfterViewInit(): void {
    this._membersService.loadMembers();
  }
  protected memberModelClicked(memberModel: MemberModel) {
    this._membersService.LikeMember(memberModel.id)
  }

}
