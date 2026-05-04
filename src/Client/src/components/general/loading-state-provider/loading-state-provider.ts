import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { LoadingIndicatorService } from '../../../services/general/loading-indicator-service';

@Component({
  selector: 'app-loading-state-provider',
  imports: [],
  templateUrl: './loading-state-provider.html',
  styleUrl: './loading-state-provider.css',
})
export class LoadingStateProvider implements AfterViewInit {

  @ViewChild('loadingModal')
  loadingModal!: ElementRef<HTMLDialogElement>;

  constructor(private loadingIndicatorService: LoadingIndicatorService) {
  }
  ngAfterViewInit(): void {
    this.loadingIndicatorService.loadingIndicatorEventTarget.addEventListener('loadingIndicator', (e: Event) => {
      let loadingEvent = e as CustomEvent<boolean>;
      if (loadingEvent.detail == true) {
        this.loadingModal.nativeElement.showModal();
      }
      else {
        this.loadingModal.nativeElement.close();
      }
    })
  }

}
