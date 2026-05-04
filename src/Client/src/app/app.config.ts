import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';
import { routes } from './app.routes';
import {authenticationIntercept, errorIntercept, loadingIntercept} from '../services/http-interceptors'
import { provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import Aura from '@primeuix/themes/aura'
import { DaisyDarkPreset, DaisyLightPreset } from '../themes/theme';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes,withViewTransitions()),
    provideHttpClient(withInterceptors([authenticationIntercept, loadingIntercept, errorIntercept])),
  ]
};
