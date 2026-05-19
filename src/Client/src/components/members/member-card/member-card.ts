import { Component, Input, output } from '@angular/core';
import { MemberModel } from '../../../models/views/member-model';
import { Gender } from '../../../models/enums/gender';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.html',
  styleUrl: './member-card.css',
  imports: [],
})
export class MemberCard {
  constructor(private router: Router) {

  }
  onMemberCardLiked = output<MemberModel>();
  @Input() public memberModel: MemberModel = new MemberModel();

  protected getCardClass(): string {
    switch (this.memberModel.gender) {
      case Gender.male: return "min-h-80 border-primary card  p-4  min-h-64 min-w-48 border-3   ";
      case Gender.female: return "min-h-80 border-secondary card p-4 min-h-64 min-w-48 border-3   ";
    }
  }
  protected getImageSource(): string {
    if (this.memberModel.profilePhotoId)
      return "https://localhost:7111/api/media/download/" + this.memberModel.profilePhotoId
    else
      return "assets/logo/unknown.png"
  }
  protected likeUser() {
    this.onMemberCardLiked.emit(this.memberModel);
  }
}
