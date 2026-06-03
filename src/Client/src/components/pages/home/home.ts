import { Component, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationStateService } from '../../../services/authentication-state-service';
import { MembersService } from '../../../services/members-service';
import { MemberCard } from "../../members/member-card/member-card";
import { MemberModel } from '../../../models/views/member-model';
import { ToastView } from '../../../models/views/toast-view';
import { Severity } from '../../../models/enums/severity';
import { ToasterService } from '../../../services/general/toaster-service';

@Component({
  selector: 'app-home',
  imports: [MemberCard],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  protected members = signal<Array<MemberModel>>([]);
  constructor(private router: Router,
    protected authStateService: AuthenticationStateService,
    protected toasterService: ToasterService,
    protected membersService: MembersService) {
  }
  ngOnInit(): void {
    if (this.authStateService.userModel() == null) {
      this.router.navigate(['/login']);
    }else {
      this.loadMembers();
    }
  }
  loadMembers() {
    // this.membersService.getMembers({ pageNumber: 1, pageSize: 3 }).subscribe({
    //   next: (res) => this.members.set(res.body)
    // })
  }
  likeMember(memberModel: MemberModel) {
    this.membersService.LikeMember(memberModel.id).subscribe({
      next: (res) => {
        if (res.isMatched) {
          let toast = new ToastView();
          toast.title = 'Congrats!';
          toast.description = 'You have a new match!';
          toast.severity = Severity.Success;
          this.toasterService.addToast(toast);
        }
        this.loadMembers();
      }
    })
  }
}

