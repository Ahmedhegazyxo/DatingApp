import { AfterViewInit, Component, ElementRef, Input, ViewChild, ViewContainerRef } from '@angular/core';
import { DialogInstanceView } from '../../../models/views/dialog-instance-view';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { EditProfileForm } from '../../profile/edit-profile-form/edit-profile-form';
import { ComponentContractType } from '../../../models/enums/component-contract-type';

@Component({
  selector: 'app-dialog-instance',
  imports: [],
  templateUrl: './dialog-instance.html',
  styleUrl: './dialog-instance.css',
})
export class DialogInstance implements AfterViewInit {

  @Input() public instance: DialogInstanceView = new DialogInstanceView('0', 'DialogTitle', () => { });
  @ViewChild('htmlDialogInstance', { static: true }) htmlDialogInstance!: ElementRef<HTMLDialogElement>;
  @ViewChild('host', { read: ViewContainerRef }) host!: ViewContainerRef;

  protected onInstanceCancelButtonClicked() {
    this.htmlDialogInstance.nativeElement.onclose
    this.instance.onCancelCallback(this.instance);
    this.dialogProvider.close(this.instance.id);
  }
  constructor(private dialogProvider: DialogProvider) {

  }
  ngAfterViewInit(): void {
    const dialog = this.htmlDialogInstance.nativeElement;

    dialog.oncancel = (e) => {
      if (dialog == e.target) {
        e.preventDefault();
        this.instance.onCancelCallback(this.instance);
        this.host.clear();
        this.dialogProvider.close(this.instance.id);
      }
    };

    dialog.showModal();

    if (!this.instance?.component) return;

    this.host.clear();
    const ref = this.host.createComponent(this.instance.component);

    if (this.instance.inputOutputdata) {
      for (const property of this.instance.inputOutputdata) {

        if (property.contractType === ComponentContractType.In) {
          ref.setInput(property.name, property.value);
        }

        if (property.contractType === ComponentContractType.Out) {
          const output = ref.instance[property.name];
          if (output?.subscribe) {
            const sub = output.subscribe(property.handler);
            ref.onDestroy(() => sub.unsubscribe());
          }
        }
      }
    }
  }

}
