import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../../services/login-service';
import { loginDto } from '../../../models/dtos/login-dto';
import { TextInput } from "../../general/shared/text-input/text-input";

@Component({
  selector: 'app-login-form',
  imports: [FormsModule, ReactiveFormsModule, TextInput],
  templateUrl: './login-form.html',
  styleUrl: './login-form.css',
})
export class LoginForm {
  protected formGroup: FormGroup = new FormGroup({});
  private fb = inject(FormBuilder);
  public loginDto: loginDto = new loginDto();
  constructor(private loginService: LoginService, private router: Router) {
    this.formGroup = this.fb.group({
      'username': ['', [Validators.required]],
      'password': ['', [Validators.required]]
    })
  }
  protected onSubmit(): void {
    if (this.formGroup.invalid)
      return;
    this.loginService.Login(this.formGroup.controls['username'].value, this.formGroup.controls['password'].value);
  }

}
