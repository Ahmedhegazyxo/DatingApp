import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHandlerFn, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { catchError, finalize, Observable, throwError } from "rxjs";
import { AuthenticationStateService } from "./authentication-state-service";
import { LoadingIndicatorService } from "./general/loading-indicator-service";
import { ErrorHandlerService } from "./general/error-handler-service";
import { ApiErrorView } from "../models/views/api-error-view";
export function authenticationIntercept(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {

    let authStateService = inject(AuthenticationStateService);
    if (authStateService.userModel == null || req.url.includes('auth')) {
        return next(req);
    }
    else {
        const authReq = req.clone({
            setHeaders: {
                Authorization: `Bearer ${authStateService.userModel()!.token}`
            }
        });
        return next(authReq);
    }
}
export function loadingIntercept(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {
    const loadingIndicatorService = inject(LoadingIndicatorService);
    loadingIndicatorService.setIsLoading(true);
    return next(req).pipe(finalize(() => {
        loadingIndicatorService.setIsLoading(false);
    }))
}
export function errorIntercept(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {
    let errorHandlerService = inject(ErrorHandlerService);
    return next(req).pipe(catchError((error: HttpErrorResponse) => {
        errorHandlerService.showErrorToaster(error);
        return throwError(error);
    }))
}