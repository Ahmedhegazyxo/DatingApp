import { Component } from '@angular/core';
import { ToastView } from '../../../models/views/ToastView';
import { Severity } from '../../../models/enums/severity';

@Component({
  selector: 'app-toaster-provider',
  imports: [],
  templateUrl: './toaster-provider.html',
  styleUrl: './toaster-provider.css',
})
export class ToasterProvider 
{
    public toastList : ToastView[] = [];
    public addToast(){
        const toast = new ToastView();
        toast.title = 'Toast Title';
        toast.callback = ()=> window.alert('aywaaa');
        toast.description = 'Toast Description';
        toast.severity = Severity.Info;
        this.toastList.push(toast);
    }
    public toastClass(sev : Severity) : string {
        switch (sev) {    
            case Severity.Info:
                return 'alert alert-info text-white shadow-lg';
            case Severity.Success: 
                return 'alert alert-success';
            case Severity.Warning:
                return 'alert alert-warning';
            case Severity.Error:
                return 'alert alert-error';
        }
    };
    public toastCallback(toaster : ToastView){
        toaster.callback();
    }
}
