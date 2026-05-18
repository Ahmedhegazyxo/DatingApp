import { AfterViewInit, Component, inject, signal } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { RegisterationService } from '../../../services/registeration-service';
import { RegisterDto } from '../../../models/dtos/register-dto';
import { TextInput } from "../../general/shared/text-input/text-input";
import { PhoneNumberInput } from "../../general/shared/phone-number-input/phone-number-input";

@Component({
  selector: 'app-registeration-form',
  imports: [FormsModule, ReactiveFormsModule, TextInput, PhoneNumberInput],
  templateUrl: './registeration-form.html',
  styleUrl: './registeration-form.css',
})
export class RegisterationForm implements AfterViewInit {
  protected formGroup: FormGroup = new FormGroup({});
  private fb = inject(FormBuilder);
  protected isFormDirty = signal(false);
  constructor(private _registerationService: RegisterationService) {
    this.formGroup =
      this.fb.group(
        {
          'email': ['', [Validators.required, Validators.email]],
          'username': ['', [Validators.required, Validators.minLength(4)]],
          'firstName': ['', [Validators.required, Validators.minLength(3)]],
          'lastName': ['', [Validators.required, Validators.minLength(3)]],
          'phoneNumber': ['', [Validators.required, Validators.minLength(3)]],
          'password': ['', [Validators.required, Validators.minLength(8)]],
          'birthdate': ['', [Validators.required]],
          'confirmPassword': ['', [Validators.required, this.matchValues('password')]],
          'gender': ['', [Validators.required]],
        });
    this.formGroup.controls['password'].valueChanges.subscribe({
      next: () => {
        this.formGroup.controls['confirmPassword'].updateValueAndValidity();
      }
    })
  }
  ngAfterViewInit(): void {
    this.formGroup?.statusChanges?.subscribe({ next: (val) => this.isFormDirty.set(this.formGroup!.dirty!) });

  }
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const parent = control.parent;
      if (!parent) return null;
      const matchValue = parent.get(matchTo)?.value;
      return control.value === matchValue ? null : { passwordMismatch: true }
    }
  }
  protected onSubmit(): void {
    if (this.formGroup.invalid)
      return;

    this._registerationService.Register(this.formGroup.value as RegisterDto);
  }
}
