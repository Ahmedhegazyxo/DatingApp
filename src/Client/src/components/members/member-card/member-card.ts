import { Component, Input, output } from '@angular/core';
import { MemberModel } from '../../../models/views/MemberModel';
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
  onMemberCardClicked = output<MemberModel>();
  @Input() public memberModel: MemberModel = new MemberModel();
  
  protected getCardClass(): string {
    switch (this.memberModel.gender) {
      case Gender.male: return "min-h-80 border-primary card  p-4  min-h-64 min-w-48 border-3   ";
      case Gender.female: return "min-h-80 border-secondary card p-4 min-h-64 min-w-48 border-3   ";
      case Gender.other: return "min-h-80 card border-ghost shadow-blue-100 p-4 min-h-64 min-w-48 border-2   ";
    }
  }
  protected getImageSource(): string {
    switch (this.memberModel.gender) {
      case Gender.male: return "assets/logo/unknown.png";
      case Gender.female: return "assets/logo/unknown.png";
      case Gender.other: return "assets/logo/unknown.png";
    }
  }
  protected likeUser() {
    this.onMemberCardClicked.emit(this.memberModel);
  }
}
