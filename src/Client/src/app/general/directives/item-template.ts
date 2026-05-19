import { AfterViewInit, Directive, ElementRef, Input, TemplateRef } from '@angular/core';

@Directive({
  selector: '[itemTemplate]',
})
export class ItemTemplate implements AfterViewInit {
  @Input() itemTemplate : string = '';
  constructor(public elementRef: ElementRef) {
  }
  ngAfterViewInit(): void {
  }
}
