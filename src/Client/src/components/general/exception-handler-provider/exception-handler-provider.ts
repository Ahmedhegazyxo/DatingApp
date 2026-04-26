import { AfterViewInit, Component, ElementRef, input, signal, ViewChild } from '@angular/core';
import { ErrorHandlerService } from '../../../services/ErrorHandlerService';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ApiErrorView } from '../../../models/views/ApiErrorView';

@Component({
  selector: 'app-exception-handler-provider',
  imports: [],
  templateUrl: './exception-handler-provider.html',
  styleUrl: './exception-handler-provider.css',
})
export class ExceptionHandlerProvider implements AfterViewInit {
  @ViewChild('errorModal', { static: true }) errorModal!: ElementRef<HTMLDialogElement>;
  constructor(private errorHandlerService: ErrorHandlerService, private router: Router) {
  }
  ngAfterViewInit(): void {
    this.errorHandlerService.errorHandlerEventTarget.addEventListener('errorEvent', (e: Event) => {
      let errorEvent = e as CustomEvent<HttpErrorResponse>;
      let apiError = errorEvent.detail.error as ApiErrorView;
      try
      {

        
        this.errorTitle.set(apiError.status.toString());
        this.errorMessage.set(apiError.title);
        this.errorStatusCode.set(apiError.status);
        this.errorModal.nativeElement.showModal();
      }
      catch
      {
        this.errorTitle.set('Internal Server Error');
        this.errorMessage.set('An unknown error has occured. Please try again later or contact support if the problem persists.');
        this.errorStatusCode.set(500);
        this.errorModal.nativeElement.showModal();
      }
    });
    this.errorModal.nativeElement.addEventListener('cancel', (e: Event) =>
      e.preventDefault());
  }
  protected errorTitle = signal<string>('');
  protected errorMessage = signal<string>('');
  protected errorStatusCode = signal<number>(0);
  protected navigateToLogin() {
    this.router.navigate(['/login']);
    this.errorModal.nativeElement.close();
  }
}