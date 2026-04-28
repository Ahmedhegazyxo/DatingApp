import { ElementRef, Injectable, signal, ViewChild, viewChild } from "@angular/core";
@Injectable({
    providedIn: 'root'
})
export class LoadingIndicatorService {
    private _isLoading = signal<boolean>(false);
    public isLoading = this._isLoading.asReadonly();
    public loadingIndicatorEventTarget: EventTarget = new EventTarget();
    constructor() {
        this.loadingIndicatorEventTarget.addEventListener('loadingIndicator', (e: Event) => {
            let loadingEvent = e as CustomEvent<boolean>;
            this._isLoading.set(loadingEvent.detail);
        })
    }
    public setIsLoading(loading: boolean): void {
        let loadingEvent: CustomEvent<boolean> = new CustomEvent<boolean>('loadingIndicator', {
            detail: loading
        });
        this.loadingIndicatorEventTarget.dispatchEvent(loadingEvent)
    }
}