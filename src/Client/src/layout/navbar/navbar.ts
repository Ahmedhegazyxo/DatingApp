import { Component, computed, Input, OnInit, signal } from '@angular/core';
import { Avatar } from '../avatar/avatar';
import { UserModel } from '../../models/views/UserModel';
import { LoginService } from '../../services/login-service';
import { RouterLinkActive, RouterLinkWithHref } from "@angular/router";
import { AcessabilityService } from '../../services/acessability-service';

@Component({
  selector: 'app-navbar',
  imports: [Avatar, RouterLinkActive, RouterLinkWithHref],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  constructor(private loginService : LoginService,protected accessabilityService : AcessabilityService) {
    
  }
  @Input() public userModel = signal<UserModel | null>(null);
  protected username = computed<string | null>(()=>{
    return this.userModel()?.username ?? null;
  });
  protected logout(){
    this.loginService.Logout(); 
  }
  protected usernameAbbreviation = computed<string | null>(() => {
    if (this.userModel() == null) {
      return null;
    }
    else {
      return this.userModel()!.firstName.charAt(0).toUpperCase() + this.userModel()!.lastName.charAt(0).toUpperCase();
    }
  })
}
