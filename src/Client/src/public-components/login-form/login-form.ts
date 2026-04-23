import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { loginDto } from '../../models/dtos/loginDto';
import { LoginService } from '../../services/LoginService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  imports: [FormsModule],
  templateUrl: './login-form.html',
  styleUrl: './login-form.css',
})
export class LoginForm {
  constructor(private loginService: LoginService, private router: Router) { }
  public loginDto: loginDto = new loginDto();
  protected onSubmit(): void {
    this.loginService.Login(this.loginDto.Username, this.loginDto.Password);
  }

}
