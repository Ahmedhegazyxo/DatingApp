import { Component, OnInit, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Navbar } from '../layout/navbar/navbar';
import { AuthenticationStateService } from '../services/authentication-state-service';
import { UserModel } from '../models/views/user-model';
import { LoadingStateProvider } from '../components/general/loading-state-provider/loading-state-provider';
import { ExceptionHandlerProvider } from '../components/general/exception-handler-provider/exception-handler-provider';
import { ToasterProvider } from "../components/general/toaster-provider/toaster-provider";
import { AcessabilityService } from '../services/general/acessability-service';
import { Dialogs } from "../components/general/dialogs/dialogs";
import { Footer } from "../components/layout/footer/footer";
import { SignalrService } from '../services/signalr-service';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastView } from '../models/views/toast-view';
import { ToasterService } from '../services/general/toaster-service';
import { Severity } from '../models/enums/severity';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, LoadingStateProvider, ExceptionHandlerProvider, ToasterProvider, Dialogs, Footer],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  
  constructor(protected authenticationStateService: AuthenticationStateService,
    protected router: Router,
    protected toasterService: ToasterService,
    private acessabilityService: AcessabilityService) {
  }
  ngOnInit(): void {
    this.setUp();
  }
  protected onDragOver(e: DragEvent) {
    e.preventDefault();
  }

  private setUp(): void {
    var theme = localStorage.getItem('data-theme');
    if (theme == null) {
      this.acessabilityService.isDarkMode.set(false);
      document.documentElement.setAttribute('data-theme', 'light');
    }
    else if (theme == 'light') {
      this.acessabilityService.isDarkMode.set(false);
      document.documentElement.setAttribute('data-theme', theme);
    }
    else {
      this.acessabilityService.isDarkMode.set(true);
      document.documentElement.setAttribute('data-theme', theme);
    }
  }
}
