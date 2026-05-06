import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UpdateProfileDto } from '../../../models/dtos/update-profile-dto';
import { ProfileService } from '../../../services/profile-service';
import { DialogProvider } from '../../../services/general/dialog-provider';

@Component({
  selector: 'app-edit-profile-form',
  imports: [FormsModule],
  templateUrl: './edit-profile-form.html',
  styleUrl: './edit-profile-form.css',
})
export class EditProfileForm {
  constructor(private profileService: ProfileService, private dialogProvider: DialogProvider) {

  }
  @Output() onValidSubmitAction = new EventEmitter();
  protected profileModel: UpdateProfileDto = new UpdateProfileDto();
  protected editProfileSubmitted(): void 
  {
    this.profileService.updateProfileBasicInfo(this.profileModel);
    let dialogId = this.dialogProvider.getCurrentDialogInstance();
    this.dialogProvider.close(dialogId!);
  }
}
