import { Component } from '@angular/core';
import { ToastView } from '../../../models/views/ToastView';

@Component({
  selector: 'app-toaster-provider',
  imports: [],
  templateUrl: './toaster-provider.html',
  styleUrl: './toaster-provider.css',
})
export class ToasterProvider 
{
  protected toasList : Array<ToastView>[] = [];
}
