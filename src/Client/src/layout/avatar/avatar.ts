import { Component, Input, OnInit, signal } from '@angular/core';
import { UserModel } from '../../models/views/UserModel';
import { AuthenticationStateService } from '../../services/AuthneticationStateService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-avatar',
  imports: [],
  templateUrl: './avatar.html',
  styleUrl: './avatar.css',
})
export class Avatar  {
  @Input() public userAbbreviation!: string | null;
  @Input() public username!: string | null;
  constructor(private router: Router) {
  }
  profileClicked() {
    this.router.navigate(['/profile']);
  }
}
