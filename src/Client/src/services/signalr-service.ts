import { Injectable } from '@angular/core';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastView } from '../models/views/toast-view';
import { Severity } from '../models/enums/severity';
import { ToasterService } from './general/toaster-service';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  constructor(private toasterService: ToasterService) {
  }
  public hubConnection?: HubConnection;
  private audio = new Audio("/assets/sound/notification.wav");

  public connect(accessToken: string) {
    this.setupSignalR(accessToken);
    this.hubConnection?.start();
  }
  public disconnect() {
    this.hubConnection?.stop();
  }
  private setupSignalR(accessToken: string) {
    this.hubConnection = new HubConnectionBuilder().withUrl("https://localhost:7111/hub",
      {
        headers: {
          Authorization: `Bearer ${accessToken}`
        },
        accessTokenFactory: () => accessToken,
        transport : HttpTransportType.ServerSentEvents
      }).withAutomaticReconnect().build();
  }
}
