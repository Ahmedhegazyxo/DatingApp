import { Component, signal } from '@angular/core';
import { ToastView } from '../../../models/views/ToastView';
import { Severity } from '../../../models/enums/severity';
import { ToasterService } from '../../../services/toaster-service';

@Component({
  selector: 'app-toaster-provider',
  imports: [],
  templateUrl: './toaster-provider.html',
  styleUrl: './toaster-provider.css',
})
export class ToasterProvider 
{
    constructor(protected toasterService : ToasterService){} 
    public toastClass(sev : Severity) : string {
        switch (sev) {    
            case Severity.Info:
                return 'alert alert-info  shadow-lg cursor-pointer  border-2 p-3';
            case Severity.Success: 
                return 'alert alert-success  shadow-lg cursor-pointer  border-2 p-3';
            case Severity.Warning:
                return 'alert alert-warning  shadow-lg cursor-pointer  border-2 p-3';
            case Severity.Error:
                return 'alert alert-error  shadow-lg cursor-pointer  border-2 p-3';
        }
    };
    public toastCallback(toaster : ToastView){
        toaster.callback(toaster);
    }
}
