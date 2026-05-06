import { Component, computed, Input, OnInit, signal } from '@angular/core';
import { Avatar } from '../avatar/avatar';
import { UserModel } from '../../models/views/user-model';
import { LoginService } from '../../services/login-service';
import { RouterLinkActive, RouterLinkWithHref } from "@angular/router";
import { AcessabilityService } from '../../services/general/acessability-service';
import { AuthenticationStateService } from '../../services/authentication-state-service';

@Component({
  selector: 'app-navbar',
  imports: [Avatar, RouterLinkActive, RouterLinkWithHref],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
   
  protected usernameAbbreviation = computed<string | null>(() => {
    if (this.authenticationStateService.userModel() == null)
      return null
    else
      return this.authenticationStateService.userModel()!.firstName.charAt(0).toUpperCase() + this.authenticationStateService.userModel()!.lastName.charAt(0).toUpperCase()

  })
  constructor(private loginService: LoginService,
     protected accessabilityService: AcessabilityService,
    protected authenticationStateService : AuthenticationStateService) {

  }

  protected logout() {
    this.loginService.Logout();
  }
}
