import { Component, Input, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-avatar',
  imports: [],
  templateUrl: './avatar.html',
  styleUrl: './avatar.css',
})
export class Avatar  {
  @Input() public userAbbreviation!: string | null;
  constructor(private router: Router) {
  }
  profileClicked() {
    this.router.navigate(['/profile']);
  }
}
