import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UpdateProfileDto } from '../../../models/dtos/updateProfileDto';
import { ProfileService } from '../../../services/profile-service';

@Component({
  selector: 'app-edit-profile-form',
  imports: [FormsModule],
  templateUrl: './edit-profile-form.html',
  styleUrl: './edit-profile-form.css',
})
export class EditProfileForm {
  constructor(private profileService: ProfileService) {

  }
  @Output() onValidSubmitAction = new EventEmitter();
  protected profileModel: UpdateProfileDto = new UpdateProfileDto();
  protected isVisible = signal(false)
  protected editProfileSubmitted(): void {
  }
  public showEditModal(model: UpdateProfileDto) {
    this.profileModel = model;
    this.isVisible.set(true);
  }
  protected handleValidSubmit() {
    this.profileService.updateProfileBasicInfo(this.profileModel);
    this.onValidSubmitAction.emit();
    this.isVisible.set(false);
  }
}
