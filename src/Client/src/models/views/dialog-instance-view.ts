import { Type } from "@angular/core";
import { ComponentContractType } from "../enums/component-contract-type";

export class DialogInstanceView {
    public id: string = '';
    public onCancelCallback: (instance: DialogInstanceView) => void = () => { };
    public title: string = 'Dialog Title';
    public component?: Type<any>
    public inputOutputdata?: Array<ComponentInputOutputContract>;
    constructor(id: string, title: string, cancelCallback: (dialogInstance: DialogInstanceView) => void, component?: Type<any>, inputOutputdata?: Array<ComponentInputOutputContract>) {
        this.id = id;
        this.title = title;
        this.onCancelCallback = cancelCallback;
        this.component = component;
        this.inputOutputdata = inputOutputdata;
    }
}
export class ComponentInputOutputContract {
    name: string = '';
    value?: any;
    handler? : (e : any) => void;
    contractType : ComponentContractType = ComponentContractType.In;
}