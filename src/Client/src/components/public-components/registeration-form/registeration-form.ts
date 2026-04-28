import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterationService } from '../../../services/registeration-service';
import { RegisterDto } from '../../../models/dtos/RegisterDto';

@Component({
  selector: 'app-registeration-form',
  imports: [FormsModule],
  templateUrl: './registeration-form.html',
  styleUrl: './registeration-form.css',
})
export class RegisterationForm {
  protected _registerDto: RegisterDto = new RegisterDto();
  constructor(private _registerationService: RegisterationService) {
  }
  protected onSubmit(): void {
    this._registerationService.Register(this._registerDto);
  }
}
