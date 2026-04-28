import { AfterViewInit, Component, computed, signal } from '@angular/core';
import { ProfileService } from '../../../services/profile-service';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements AfterViewInit {

  protected isLoading = signal<boolean>(false);

  protected isInputDisabled = signal<boolean>(true);

  protected age: any;
  constructor(public profileService: ProfileService) {
  }
  ngAfterViewInit(): void {
    this.profileService.getProfileInfo();
    this.age = 5;
  }
  protected onEditButtonChanged() {
    this.isInputDisabled.set(false);
  }
  protected onCancelChanges() {
    this.isInputDisabled.set(true);
  }

}
