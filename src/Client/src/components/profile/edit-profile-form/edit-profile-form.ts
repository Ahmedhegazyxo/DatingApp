import { AfterViewInit, Component, computed, EventEmitter, inject, Input, OnChanges, OnInit, Output, signal, SimpleChanges, ViewChild } from '@angular/core';
import { ReactiveFormsModule, NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { UpdateProfileDto } from '../../../models/dtos/update-profile-dto';
import { ProfileService } from '../../../services/profile-service';
import { DialogProvider } from '../../../services/general/dialog-provider';
import { AuthenticationStateService } from '../../../services/authentication-state-service';
import { ProfileModel } from '../../../models/views/profile-model';
import { TextInput } from "../../general/shared/text-input/text-input";
import { TextAreaInput } from "../../general/shared/text-area-input/text-area-input";
import { PhoneNumberInput } from "../../general/shared/phone-number-input/phone-number-input";

@Component({
  selector: 'app-edit-profile-form',
  imports: [ReactiveFormsModule, TextInput, TextAreaInput, PhoneNumberInput],
  templateUrl: './edit-profile-form.html',
  styleUrl: './edit-profile-form.css',
})
export class EditProfileForm implements AfterViewInit, OnInit {

  protected editForm = new FormGroup({
    email: new FormControl(),
    username: new FormControl(),
    firstName: new FormControl(),
    lastName: new FormControl(),
    phoneNumber: new FormControl(),
    birthdate: new FormControl(),
    biography: new FormControl(),
    gender: new FormControl(),
  });
  @Output() afterValidSubmitAction = new EventEmitter<ProfileModel>();
  @Input() protected updateProfileDto: UpdateProfileDto = new UpdateProfileDto();
  @Input() protected profileModel: ProfileModel = new ProfileModel();

  protected isFormDirty = signal<boolean>(false);

  constructor(private profileService: ProfileService,
    private dialogProvider: DialogProvider,
    private authStateService: AuthenticationStateService) {

  }

  ngOnInit(): void {
    this.editForm = new FormGroup({
      email: new FormControl(this.profileModel.email, [Validators.required, Validators.email]),
      username: new FormControl(this.profileModel.username, [Validators.required, Validators.minLength(2), Validators.maxLength(20)]),
      firstName: new FormControl(this.profileModel.firstName, Validators.required),
      lastName: new FormControl(this.profileModel.lastName, Validators.required),
      phoneNumber: new FormControl(this.profileModel.phoneNumber, [Validators.required, Validators.minLength(11), Validators.pattern(/^\+?\d+$/)]),
      birthdate: new FormControl(this.profileModel.birthdate.slice(0, 10), Validators.required),
      biography: new FormControl(this.profileModel.biography, Validators.maxLength(512)),
      gender: new FormControl(this.profileModel.gender, Validators.required),
    });
  }
  ngAfterViewInit(): void {
    this.editForm?.statusChanges?.subscribe({ next: (val) => this.isFormDirty.set(this.editForm!.dirty!) });

  }

  protected editProfileSubmitted(): void {
    if (this.editForm.invalid)
      return;

    this.profileService.updateProfileBasicInfo(this.editForm.value as UpdateProfileDto).subscribe({
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
