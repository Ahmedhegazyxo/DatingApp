import { AfterViewInit, Component, ElementRef, EventEmitter, Input, Output, signal, ViewChild } from '@angular/core';
import { ProfileService } from '../../../services/profile-service';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-update-profile-photo-form',
  imports: [FormsModule],
  templateUrl: './update-profile-photo-form.html',
  styleUrl: './update-profile-photo-form.css',
})
export class UpdateProfilePhotoForm implements AfterViewInit {
  @ViewChild('editForm') protected editForm?: NgForm;
  @ViewChild('profileCanvas') profileCanvas!: ElementRef<HTMLCanvasElement>
  protected file: File | null = null;
  protected imgSrc = signal<string>('/assets/logo/unknown.png');
  @Output() protected afterValidSubmitAction = new EventEmitter();

  constructor(private profileService: ProfileService) {

  }

  ngAfterViewInit(): void {
    if (this.profileService.profileModel()?.profilePhotoId != null) {

      this.imgSrc.set("https://localhost:7111/api/media/download/" + this.profileService.profileModel()?.profilePhotoId)
    }
  }
  protected previewImage(e: Event) {
    const input = e.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) {
      return;
    }
    this.file = input.files[0];
    this.imgSrc.set(URL.createObjectURL(this.file));

  }
  protected onValidSubmitAction() {
    if (this.file)
      this.profileService.updateProfilePhoto(this.file).subscribe({
        next: () =>  this.afterValidSubmitAction.emit()
      })
  }
}