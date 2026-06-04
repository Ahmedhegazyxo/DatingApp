import { Component, computed, inject, Input, signal } from '@angular/core';
import { ChatMessageDto } from '../../../models/views/chat-message-view';
import { AuthenticationStateService } from '../../../services/authentication-state-service';
import { ChatMessagesService } from '../../../services/chat-messages-service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { SignalrService } from '../../../services/signalr-service';
import { ToastView } from '../../../models/views/toast-view';
import { ToasterService } from '../../../services/general/toaster-service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-chat',
  imports: [ReactiveFormsModule, FormsModule,DatePipe],
  templateUrl: './chat.html',
  styleUrl: './chat.css',
})
export class Chat {
  protected formGroup: FormGroup = new FormGroup({});
  private fb = inject(FormBuilder);
  private audio = new Audio('/assets/sound/notification.wav');
  @Input() public matchId: string = '';
  constructor(protected authenticationStateService: AuthenticationStateService,
    protected chatMessagesService: ChatMessagesService,
    private signalRService: SignalrService,
    private toasterService: ToasterService
  ) {
    this.formGroup = this.fb.group({
      'messageBody': ['', [Validators.required]],
    });
    console.log("Registered the handler");
    console.log(this.signalRService.hubConnection);
    this.signalRService.connect(this.authenticationStateService.userModel()!.token);
    this.signalRService.hubConnection?.on("RecievedMessage", (notification) => {
      console.log(notification);
      let toast = new ToastView(notification.title, notification.body, notification.severity, 5000);
      let chatMessageDto: ChatMessageDto = new ChatMessageDto();
      chatMessageDto.senderId = notification.senderId
      chatMessageDto.sentAt = Date.now().toString();
      chatMessageDto.messageBody = notification.body;
      this.messages.update((old) => [chatMessageDto].concat(old));
      this.toasterService.addToast(toast);
      this.audio.pause();
      this.audio.currentTime = 0;
      this.audio.play().then(() => {
      })
        .catch((err) => {
          console.error(err);
        })
    })
  }
  currentPageNumber = 1;
  protected messages = signal<ChatMessageDto[]>([]);
  protected messagesReversed = computed(() => {
    if (this.messages() != null) {
      return this.messages()!.slice().reverse();
    }
    return [];
  });
  protected ngOnInit(): void {
    this.loadMessages();
  }
  protected loadMessages() {
    this.chatMessagesService.getMatchMessageThread(this.matchId, { pageNumber: this.currentPageNumber, pageSize: 7 }).subscribe({
      next: (response) => {
        this.messages.set(response.body);
      }
    });
  }
  protected loadMore() {
    this.currentPageNumber++;
    if (this.messages() != null) {
      let currentLength = this.messages()!.length;
      this.chatMessagesService.getMatchMessageThread(this.matchId, { pageNumber: this.currentPageNumber, pageSize: 7 }).subscribe({
        next: (response) => {
          this.messages.update((old) => (old.concat(response.body)));
        }
      });
    }
  }
  protected onSubmit(): void {
    if (this.formGroup.invalid) {
      return;
    }
    this.chatMessagesService.sendMessage(this.matchId, this.formGroup.controls['messageBody'].value).subscribe({
      next: (val) => {
        if (val.isSent) {
          const message = this.formGroup.controls['messageBody'].value;
          let chatMessageDto: ChatMessageDto = new ChatMessageDto();
          chatMessageDto.senderId = this.authenticationStateService.userModel()!.id;
          chatMessageDto.sentAt = val.timeSent;
          chatMessageDto.messageBody = message;
          this.messages.update((old) => [chatMessageDto].concat(old));
          this.formGroup.reset();
        }
      }
    })
  }
}
