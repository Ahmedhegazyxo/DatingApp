import { AfterViewInit, Component, computed, signal, ViewChild } from '@angular/core';
import { ProfileService } from '../../../services/profile-service';
import { ProfilePreferences } from "../../profile/profile-preferences/profile-preferences";
import { DatePipe } from '@angular/common';
import { EditProfileForm } from '../../profile/edit-profile-form/edit-profile-form';
import { UpdateProfileDto } from '../../../models/dtos/updateProfileDto';

@Component({
  selector: 'app-profile',
  imports: [ProfilePreferences, DatePipe, EditProfileForm],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements AfterViewInit {
  @ViewChild(EditProfileForm) editProfileForm!: EditProfileForm;
  private updateProfileBasicInfoDto: UpdateProfileDto = new UpdateProfileDto();
  protected isLoading = signal<boolean>(false);


  protected age: any;
  constructor(public profileService: ProfileService) {
  }
  ngAfterViewInit(): void {
    this.profileService.getProfileInfo();
    this.age = 5;
  }
  protected editbuttonClicked() {
    this.updateProfileBasicInfoDto.username = this.profileService.profileModel()!.username;
    this.updateProfileBasicInfoDto.firstName = this.profileService.profileModel()!.firstName;
    this.updateProfileBasicInfoDto.lastName = this.profileService.profileModel()!.lastName;
    this.updateProfileBasicInfoDto.phoneNumber = this.profileService.profileModel()!.phoneNumber;
    this.updateProfileBasicInfoDto.birthdate = new Date(this.profileService.profileModel()!.birthdate);
    this.editProfileForm.showEditModal(this.updateProfileBasicInfoDto);
  }
  protected profileUpdated() {
    this.profileService.getProfileInfo();
  }
}
