import { AfterViewChecked, Component, OnInit, Signal, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Navbar } from '../layout/navbar/navbar';
import { AuthenticationStateService } from '../services/AuthneticationStateService';
import { UserModel } from '../models/views/UserModel';
import { LoadingStateProvider } from '../components/general/loading-state-provider/loading-state-provider';
import { ExceptionHandlerProvider } from '../components/general/exception-handler-provider/exception-handler-provider';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, LoadingStateProvider, ExceptionHandlerProvider],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected userModel = signal<UserModel | null>(null);
  constructor(private authenticationStateService: AuthenticationStateService, protected router: Router) { }
 
  ngOnInit(): void {
    this.setUp();
    let user = this.authenticationStateService.getUserModelInfo();
    this.userModel.set(user);
  }

  private setUp(): void {
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
