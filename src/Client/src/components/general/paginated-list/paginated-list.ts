import { AfterViewInit, Component, ContentChild, Input, input, signal, TemplateRef } from '@angular/core';
import { Paginator } from "../paginator/paginator";
import { PaginationFilter } from '../../../models/queries/paginationFilter';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../../../models/views/paginated-result';
import { NgTemplateOutlet } from '@angular/common';

@Component({
  selector: 'app-paginated-list',
  imports: [Paginator, NgTemplateOutlet],
  templateUrl: './paginated-list.html',
  styleUrl: './paginated-list.css',
})
export class PaginatedList<T> implements AfterViewInit {
  public pageSize = input<number>(50);
  @Input() fetchFn!: (filter: PaginationFilter) => Observable<PaginatedResult<T>>;
  @Input() itemTemplate!: TemplateRef<any>
  @Input() currentPageNumber = signal<number>(1);
  @Input() currentPageSize = signal<number>(2);
  protected totalItemsCount = signal<number>(0);
  protected paginatedResult = signal<PaginatedResult<T>>(new PaginatedResult<T>());
  protected pageNumberChanged(pageNumber: number) {
    this.currentPageNumber.set(pageNumber);
    this.load();
  }
  protected pageSizeChanged(pageSize: number) {
    this.currentPageSize.set(pageSize);
    this.load()
  }
  ngAfterViewInit(): void {
    this.load();
  }
  public refresh() {
    this.load();
  }
  public load() {
    this.fetchFn({ pageNumber: this.currentPageNumber(), pageSize: this.currentPageSize() })
      .subscribe({
        next: (pr) => {
          this.paginatedResult.set(pr);
          this.totalItemsCount.set(pr.totalCount);
        }
      })
  }
}