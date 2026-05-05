import { Component, Injectable, signal, Type } from '@angular/core';
import { DialogInstanceView } from '../../models/views/dialog-instance-view';

@Injectable({
  providedIn: 'root',
})
export class DialogProvider {
  public target: EventTarget = new EventTarget();
  public dialogInstances = signal<Array<DialogInstanceView>>([]);
  public open(title: string, cancelCallback: (dialogInstance: DialogInstanceView) => void, component?: Type<any>): DialogInstanceView {
    var instance = new DialogInstanceView(this.randomString(), title, cancelCallback, component);
    this.dialogInstances.update(list => [...list,instance]);
    return instance;
  }
  public close(id: string) {
    this.dialogInstances.update(list => [...list.filter(e => e.id != id)])
  }
  randomString(length = 5): string {
    return Math.random().toString(36).substring(2, 2 + length);
  }
  public getCurrentDialogInstance(): string | undefined {
    return this.dialogInstances().at(-1)?.id;
  }
}
