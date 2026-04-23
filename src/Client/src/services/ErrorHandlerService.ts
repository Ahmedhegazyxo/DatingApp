import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiErrorView } from "../models/views/ApiErrorView";

@Injectable({
    providedIn: 'root'
})
export class ErrorHandlerService {

    public errorHandlerEventTarget: EventTarget = new EventTarget();
    public showErrorToaster(error: ApiErrorView) {
        let errorEvent = new CustomEvent<ApiErrorView>('errorEvent', {
            detail: error
        })
        this.errorHandlerEventTarget.dispatchEvent(errorEvent);
    }
}