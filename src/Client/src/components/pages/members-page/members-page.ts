import { Component } from '@angular/core';
import { MembersList } from '../../members/members-list/members-list';

@Component({
  selector: 'app-members-page',
  imports: [MembersList],
  templateUrl: './members-page.html',
  styleUrl: './members-page.css',
})
export class MembersPage {}
