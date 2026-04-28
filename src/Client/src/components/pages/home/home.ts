import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationStateService } from '../../../services/authentication-state-service';
import { MembersList } from '../../members/members-list/members-list';

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

