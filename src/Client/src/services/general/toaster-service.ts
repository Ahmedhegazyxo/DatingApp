import { Injectable, signal } from '@angular/core';
import { ToastView } from '../../models/views/ToastView';

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  public toastList = signal<ToastView[]>([]);
  public addToast(toast: ToastView) {
    this.toastList.update((current) => [...current, toast]);

    setTimeout(() => {
      this.removeToast(toast);
    }, toast.duration);

  }
  public removeToast(toast: ToastView) {
    this.toastList.update((current) => current.filter((t) => t !== toast));
  }
}
