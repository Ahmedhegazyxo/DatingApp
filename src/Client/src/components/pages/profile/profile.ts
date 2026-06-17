import { AfterViewInit, Component, computed, Inject, signal, ViewChild } from '@angular/core';
import { ProfileService } from '../../../services/profile-service';
import { ProfilePreferences } from "../../profile/profile-preferences/profile-preferences";
import { DatePipe, JsonPipe } from '@angular/common';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { EditProfileForm } from '../../profile/edit-profile-form/edit-profile-form';
import { ComponentContractType } from '../../../models/enums/component-contract-type';
import { ToasterService } from '../../../services/general/toaster-service';
import { ToastView } from '../../../models/views/toast-view';
import { Severity } from '../../../models/enums/severity';
import { UpdateProfilePhotoForm } from '../../profile/update-profile-photo-form/update-profile-photo-form';
import { ProfileMetricsView } from '../../../models/views/profile-metrics-view';

@Component({
  selector: 'app-profile',
  imports: [ProfilePreferences, DatePipe],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements AfterViewInit {
  protected isLoading = signal<boolean>(false);
  protected profileMetrics = signal<ProfileMetricsView | null>(null);

  protected age: any;
  constructor(public profileService: ProfileService,
    private dialogProviderService: DialogProvider,
    private toasterService: ToasterService
  ) {
  }
  ngAfterViewInit(): void {
    this.profileService.getProfileInfo();
    this.profileService.getProfileMetrics().subscribe({
      next: (res) => {
        console.log(res);
        this.profileMetrics.set(res);
      }
    });
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
