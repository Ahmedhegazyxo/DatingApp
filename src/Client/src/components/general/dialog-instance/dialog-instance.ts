import { AfterViewInit, Component, ElementRef, Input, ViewChild, ViewContainerRef } from '@angular/core';
import { DialogInstanceView } from '../../../models/views/DialogInstanceView';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { EditProfileForm } from '../../profile/edit-profile-form/edit-profile-form';

@Component({
  selector: 'app-dialog-instance',
  imports: [],
  templateUrl: './dialog-instance.html',
  styleUrl: './dialog-instance.css',
})
export class DialogInstance implements AfterViewInit {

  @Input() public instance: DialogInstanceView = new DialogInstanceView('0', 'DialogTitle', () => { });
  @ViewChild('htmlDialogInstance', { static: true }) htmlDialogInstance!: ElementRef<HTMLDialogElement>;
  @ViewChild('host', { read: ViewContainerRef })
  private host!: ViewContainerRef;

  protected onInstanceCancelButtonClicked() {
    this.htmlDialogInstance.nativeElement.onclose
    this.instance.onCancelCallback(this.instance);
    this.dialogProvider.close(this.instance.id);
  }
  constructor(private dialogProvider: DialogProvider) {

  }
  ngAfterViewInit(): void {
    this.htmlDialogInstance.nativeElement.showModal();
    if (this.instance != null)
      this.host.createComponent(this.instance.component!);
    this.htmlDialogInstance.nativeElement.oncancel = (e) => {
      e.preventDefault();
      this.instance.onCancelCallback(this.instance);
      this.dialogProvider.close(this.instance.id);
    }
  }

}
