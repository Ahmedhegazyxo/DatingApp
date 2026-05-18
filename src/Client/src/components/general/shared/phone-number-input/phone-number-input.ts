import { Component, input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';
import { ValidationHints } from "../validation-hints/validation-hints";

@Component({
  selector: 'app-phone-number-input',
  imports: [ReactiveFormsModule, ValidationHints],
  templateUrl: './phone-number-input.html',
  styleUrl: './phone-number-input.css',
})
export class PhoneNumberInput implements ControlValueAccessor {
  label = input<string>('Field');
  inputClass = input<string>('');
  placeholder = input<string>('');
  constructor(@Self() public ngControl: NgControl) {
    ngControl.valueAccessor = this;

  }

  private strip(value: string): string {
    return value.replace(/\D/g, '');
  }

  private format(value: string): string {
    const digits = this.strip(value);

    if (digits.length <= 3) return digits;
    if (digits.length <= 7) return `${digits.slice(0, 3)}-${digits.slice(3)}`;
    return `${digits.slice(0, 3)}-${digits.slice(3, 7)}-${digits.slice(7)}`;
  }
  onTouched() {

  }
  onChange() {
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }
  setDisabledState?(isDisabled: boolean): void {
  }
  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }
}
