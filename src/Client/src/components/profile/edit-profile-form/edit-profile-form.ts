import { AfterViewInit, Component, computed, EventEmitter, inject, Input, OnChanges, OnInit, Output, signal, SimpleChanges, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UpdateProfileDto } from '../../../models/dtos/update-profile-dto';
import { ProfileService } from '../../../services/profile-service';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { AuthenticationStateService } from '../../../services/authentication-state-service';
import { ProfileModel } from '../../../models/views/profile-model';
import { Avatar } from '../../../layout/avatar/avatar';

@Component({
  selector: 'app-edit-profile-form',
  imports: [FormsModule],
  templateUrl: './edit-profile-form.html',
  styleUrl: './edit-profile-form.css',
})
export class EditProfileForm implements AfterViewInit, OnInit {
  @ViewChild('editForm') protected editForm?: NgForm;

  @Output()
  afterValidSubmitAction = new EventEmitter<ProfileModel>();
  @Input() protected updateProfileDto: UpdateProfileDto = new UpdateProfileDto();
  @Input() protected profileModel: ProfileModel = new ProfileModel();

  protected isFormDirty = signal<boolean>(false);

  constructor(private profileService: ProfileService,
    private dialogProvider: DialogProvider,
    private authStateService: AuthenticationStateService) {

  }

  ngOnInit(): void {
    this.updateProfileDto.username = this.profileModel.username;
    this.updateProfileDto.firstName = this.profileModel.firstName;
    this.updateProfileDto.lastName = this.profileModel.lastName;
    this.updateProfileDto.phoneNumber = this.profileModel.phoneNumber;
    this.updateProfileDto.biography = this.profileModel.biography;
    this.updateProfileDto.gender = this.profileModel.gender;
    this.updateProfileDto.birthdate = this.profileModel.birthdate.slice(0,10);
  }
  ngAfterViewInit(): void {
    this.editForm?.statusChanges?.subscribe({ next: (val) => this.isFormDirty.set(this.editForm!.dirty!) });
  }


  protected editProfileSubmitted(): void {
    this.profileService.updateProfileBasicInfo(this.updateProfileDto).subscribe({
      next: (profileUpdated) => {
        this.profileService.profileModel.set(profileUpdated);
        var userModel = this.authStateService.userModel();
        userModel!.username = profileUpdated.username;
        userModel!.firstName = profileUpdated.firstName;
        userModel!.lastName = profileUpdated.lastName;
        this.authStateService.setUserModelInfo(userModel!);
        this.afterValidSubmitAction.emit(profileUpdated);
      }
    });
  }
}
