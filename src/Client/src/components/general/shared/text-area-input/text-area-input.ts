import { Component, input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';
import { ValidationHints } from "../validation-hints/validation-hints";

@Component({
  selector: 'app-text-area-input',
  imports: [ValidationHints,ReactiveFormsModule],
  templateUrl: './text-area-input.html',
  styleUrl: './text-area-input.css',
})
export class TextAreaInput implements ControlValueAccessor {
  label = input<string>('');
  inputClass = input<string>('');
  placeholder = input<string>('');
  constructor(@Self() public ngControl: NgControl) {
    ngControl.valueAccessor = this;

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
