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
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, LoadingStateProvider, ExceptionHandlerProvider, ToasterProvider, Dialogs],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected userModel = signal<UserModel | null>(null);
  constructor(private authenticationStateService: AuthenticationStateService,
     protected router: Router,
     private acessabilityService : AcessabilityService) { }

  ngOnInit(): void {
    this.setUp();
    let user = this.authenticationStateService.getUserModelInfo();
    this.userModel.set(user);
  }

  private setUp(): void {
    var theme = localStorage.getItem('data-theme');
    if (theme == null){
      this.acessabilityService.isDarkMode.set(false);
      document.documentElement.setAttribute('data-theme', 'light');
    }
    else {
      this.acessabilityService.isDarkMode.set(true);
      document.documentElement.setAttribute('data-theme', theme);
    }
    this.authenticationStateService.AuhtenticationEventAppTarget.addEventListener('loginevent', (e: Event) => {
      let userEvent = e as CustomEvent<UserModel>;
      let user = userEvent.detail;
      this.userModel.set(user);
    });
    this.authenticationStateService.AuhtenticationEventAppTarget.addEventListener('logoutevent', () => {
      this.userModel.set(null);
    })
  }
}
