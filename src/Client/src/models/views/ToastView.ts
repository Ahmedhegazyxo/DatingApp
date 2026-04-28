import { Severity } from "../enums/severity";

export class ToastView 
{
    public title : string = '';
    public description : string = '';
    public duration : number = 3000;
    public severity : Severity = Severity.Info;
    public iconClass : string = 'fa fa-info-circle';
    public callback : (view: ToastView)=> void = (view)=>{};
    constructor(title?: string, description?: string, severity?: Severity, duration?: number, callback?: (view: ToastView)=> void) {
        if(title) this.title = title;
        if(description) this.description = description;
        if(severity) this.severity = severity;
        if(duration) this.duration = duration;
        if(callback) this.callback = callback;        
    }
    
 }
