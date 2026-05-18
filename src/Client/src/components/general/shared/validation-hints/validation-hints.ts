import { AfterViewInit, Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-validation-hints',
  imports: [ReactiveFormsModule],
  templateUrl: './validation-hints.html',
  styleUrl: './validation-hints.css',
})
export class ValidationHints implements AfterViewInit {

  control = input<FormControl>();
  label = input<string>('Field');
  constructor() {
  }
  ngAfterViewInit(): void {
  }
  get show() {
    return this.control()!.invalid &&
      (this.control()!.touched)
  }
}
