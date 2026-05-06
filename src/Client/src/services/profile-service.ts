import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProfileModel } from '../models/views/profile-model';
import { UpdateProfileDto } from '../models/dtos/update-profile-dto';
import { AuthenticationStateService } from './authentication-state-service';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private baseUri = 'http://localhost:5138/api/profile'
  public profileModel = signal<ProfileModel | null>(null);
  constructor(private httpClient: HttpClient
    , private authStateService: AuthenticationStateService
  ) {

  }
  public getProfileInfo() {
    this.httpClient.get<ProfileModel>(this.baseUri).subscribe({
      next: (value) => {
        console.log(value)
        this.profileModel.set(value);
        var userModel = this.authStateService.getUserModelInfo();
        console.log(userModel);
        if (userModel != null) userModel.username = value.username;
        console.log(value);
        this.authStateService.setUserModelInfo(userModel!)
        console.log(userModel);
      }
    });
  }
  public updateProfileBasicInfo(model: UpdateProfileDto) {
    this.httpClient.put<string>(this.baseUri, model).subscribe({
      next: () => {
        this.getProfileInfo();
      }
    });
  }
}
