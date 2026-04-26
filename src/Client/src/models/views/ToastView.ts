import { Severity } from "../enums/severity";

export class ToastView 
{
    public title : string = '';
    public description : string = '';
    public duration : number = 3000;
    public severity : Severity = Severity.Info;
    public toastClass() : string {
        switch (this.severity) {    
            case Severity.Info:
                return 'alert-info';
            case Severity.Success: 
                return 'alert-success';
            case Severity.Warning:
                return 'alert-warning';
            case Severity.Error:
                return 'alert-error';
        }
    };
 }
