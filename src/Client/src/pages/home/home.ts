import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from '../../models/views/UserModel';
import { MembersList } from '../../components/members/members-list/members-list';
import { AuthenticationStateService } from '../../services/AuthneticationStateService';

@Component({
  selector: 'app-home',
  imports: [MembersList],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  constructor(private router: Router, protected authStateService: AuthenticationStateService) {

  }
  ngOnInit(): void {
    if (this.authStateService.userModel() == null) {
      this.router.navigate(['/login']);
    }
  }
}

