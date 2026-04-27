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
                return 'alert alert-info text-white shadow-lg cursor-pointer';
            case Severity.Success: 
                return 'alert alert-success text-white shadow-lg cursor-pointer';
            case Severity.Warning:
                return 'alert alert-warning text-white shadow-lg cursor-pointer';
            case Severity.Error:
                return 'alert alert-error text-white shadow-lg cursor-pointer';
        }
    };
    public toastCallback(toaster : ToastView){
        toaster.callback();
    }
}
