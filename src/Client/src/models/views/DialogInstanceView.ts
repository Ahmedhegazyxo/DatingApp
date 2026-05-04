import { Type } from "@angular/core";

export class DialogInstanceView {
    public id: string = '';
    public onCancelCallback: (instance: DialogInstanceView) => void = () => { };
    public title: string = 'Dialog Title';
    public component? : Type<any>
    constructor(id: string, title: string, cancelCallback: (dialogInstance: DialogInstanceView) => void, component? : Type<any>) {
        this.id = id;
        this.title = title;
        this.onCancelCallback = cancelCallback;
        this.component = component;
    }
}