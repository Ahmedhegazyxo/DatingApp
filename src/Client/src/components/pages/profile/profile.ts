import { AfterViewInit, Component, computed, Inject, signal, ViewChild } from '@angular/core';
import { ProfileService } from '../../../services/profile-service';
import { ProfilePreferences } from "../../profile/profile-preferences/profile-preferences";
import { DatePipe } from '@angular/common';
import { UpdateProfileDto } from '../../../models/dtos/update-profile-dto';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { EditProfileForm } from '../../profile/edit-profile-form/edit-profile-form';
import { ProfileModel } from '../../../models/views/profile-model';
import { ComponentContractType } from '../../../models/enums/component-contract-type';
import { Avatar } from '../../../layout/avatar/avatar';
import { ToasterProvider } from '../../general/toaster-provider/toaster-provider';
import { ToasterService } from '../../../services/general/toaster-service';
import { ToastView } from '../../../models/views/toast-view';
import { Severity } from '../../../models/enums/severity';
import { UpdateProfilePhotoForm } from '../../profile/update-profile-photo-form/update-profile-photo-form';

@Component({
  selector: 'app-profile',
  imports: [ProfilePreferences, DatePipe],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements AfterViewInit {
  protected isLoading = signal<boolean>(false);


  protected age: any;
  constructor(public profileService: ProfileService,
    private dialogProviderService: DialogProvider,
    private toasterService: ToasterService
  ) {
  }
  ngAfterViewInit(): void {
    this.profileService.getProfileInfo();
  }
  protected editbuttonClicked() {
    var dialogInstance = this.dialogProviderService.open('Update Profile', () => { }, EditProfileForm, [
      {
        name: 'afterValidSubmitAction', contractType: ComponentContractType.Out, handler: (e) => {
          this.dialogProviderService.close(dialogInstance.id);
          this.toasterService.addToast(new ToastView('Profile', 'Profile updated successfully', Severity.Success, 3000));
        }
      },
      { name: 'profileModel', contractType: ComponentContractType.In, value: this.profileService.profileModel() }]);
  }
  protected onEditProfilePicture() {
    var dialogInstance = this.dialogProviderService.open('Update Profile Photo', () => { }, UpdateProfilePhotoForm, [{
      contractType: ComponentContractType.Out,
      name: 'afterValidSubmitAction',
      handler: (e) => { 
        this.dialogProviderService.close(dialogInstance.id);
        this.profileService.getProfileInfo();
        window.location.reload();

       }
    }]);

  }
  protected getImageSource(): string {
    if (this.profileService.profileModel()?.profilePhotoId != null)
      return "https://localhost:7111/api/media/download/" + this.profileService.profileModel()!.profilePhotoId
    else
      return "assets/logo/unknown.png"
  }
}
