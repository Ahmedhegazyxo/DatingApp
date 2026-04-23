import { Component } from '@angular/core';
import { RegisterationForm } from '../../public-components/registeration-form/registeration-form';

@Component({
  selector: 'app-register-page',
  imports: [RegisterationForm],
  templateUrl: './register-page.html',
  styleUrl: './register-page.css',
})
export class RegisterPage {}
