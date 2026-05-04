import { Component, signal, ViewChild } from '@angular/core';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { DialogInstanceView } from '../../../models/views/DialogInstanceView';
import { DialogInstance } from "../dialog-instance/dialog-instance";

@Component({
  selector: 'app-dialogs',
  imports: [DialogInstance],
  templateUrl: './dialogs.html',
  styleUrl: './dialogs.css',
})
export class Dialogs {
  constructor(protected dialogService: DialogProvider) { }
}
