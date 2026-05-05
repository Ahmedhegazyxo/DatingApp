import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiErrorView } from "../../models/views/api-error-view";

@Injectable({
    providedIn: 'root'
})
export class ErrorHandlerService {

    public errorHandlerEventTarget: EventTarget = new EventTarget();
    public showErrorToaster(error: HttpErrorResponse) {
        let errorEvent = new CustomEvent<HttpErrorResponse>('errorEvent', {
            detail: error
        });
        this.errorHandlerEventTarget.dispatchEvent(errorEvent);
    }
}