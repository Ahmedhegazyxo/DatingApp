import { Component, computed, EventEmitter, Input, Output, signal } from '@angular/core';

@Component({
  selector: 'app-paginator',
  imports: [],
  templateUrl: './paginator.html',
  styleUrl: './paginator.css',
})
export class Paginator {
  @Input() public currentPageNumber = signal<number>(1);
  @Input() public totalItemsCount = signal<number>(3);
  @Input() public pageSize = signal<number>(5);

  @Output() public pageNumberChanged = new EventEmitter<number>();
  @Output() public pageSizeChanged = new EventEmitter<number>();

  protected pages = computed(() => {
    const total = this.totalItemsCount();
    const pageSize = this.pageSize()
    const totalPages = Math.ceil(total / pageSize);
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  });

  protected onButtonClicked(pageNumber: number) {
    this.currentPageNumber.set(pageNumber);
    this.pageNumberChanged.emit(pageNumber);
  }
}
