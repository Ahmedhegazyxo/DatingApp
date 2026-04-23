import { Injectable, signal } from '@angular/core';
import { UserModel } from '../models/views/UserModel';
import { HttpClient } from '@angular/common/http';
import { ProfileModel } from '../models/views/ProfileModel';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private baseUri = 'http://localhost:5138/api/profile'
  public profileModel = signal<ProfileModel | null>(null); 
  constructor(private httpClient: HttpClient) {

  }
  public getProfileInfo() {
    this.httpClient.get<ProfileModel>(this.baseUri).subscribe({
      next: (value) => this.profileModel.set(value)
    });
  }
}
