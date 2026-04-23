import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHandlerFn, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { catchError, finalize, Observable, throwError } from "rxjs";
import { AuthenticationStateService } from "./AuthneticationStateService";
import { LoadingIndicatorService } from "./LoadingIndicatorService";
import { ErrorHandlerService } from "./ErrorHandlerService";
import { ApiErrorView } from "../models/views/ApiErrorView";
export function authenticationIntercept(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {

    let authStateService = inject(AuthenticationStateService);
    console.log('authentication status' , authStateService.userModel);
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
        let apiError = error.error as ApiErrorView;
        errorHandlerService.showErrorToaster(apiError);
        return throwError(error);
    }))
}