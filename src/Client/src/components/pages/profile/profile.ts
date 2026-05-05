import { AfterViewInit, Component, computed, Inject, signal, ViewChild } from '@angular/core';
import { ProfileService } from '../../../services/profile-service';
import { ProfilePreferences } from "../../profile/profile-preferences/profile-preferences";
import { DatePipe } from '@angular/common';
import { UpdateProfileDto } from '../../../models/dtos/update-profile-dto';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { EditProfileForm } from '../../profile/edit-profile-form/edit-profile-form';

@Component({
  selector: 'app-profile',
  imports: [ProfilePreferences, DatePipe],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements AfterViewInit {
  private updateProfileBasicInfoDto: UpdateProfileDto = new UpdateProfileDto();
  protected isLoading = signal<boolean>(false);


  protected age: any;
  constructor(public profileService: ProfileService,
    private dialogProviderService: DialogProvider
  ) {
  }
  ngAfterViewInit(): void {
    this.profileService.getProfileInfo();
  }
  protected editbuttonClicked() {
    this.dialogProviderService.open('Update Profile' , ()=>{}, EditProfileForm);
  }
  protected profileUpdated() {
    this.profileService.getProfileInfo();
  }
}
