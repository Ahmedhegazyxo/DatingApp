import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProfileModel } from '../models/views/profile-model';
import { UpdateProfileDto } from '../models/dtos/update-profile-dto';
import { AuthenticationStateService } from './authentication-state-service';
import { Observable, timeInterval } from 'rxjs';
import { ProfileMetricsView } from '../models/views/profile-metrics-view';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private baseUri = 'https://localhost:7111/api/profile'
  public profileModel = signal<ProfileModel | null>(null);
  constructor(private httpClient: HttpClient
    , private authStateService: AuthenticationStateService
  ) {

  }
  public getProfileInfo() {
    this.httpClient.get<ProfileModel>(this.baseUri).subscribe({
      next: (value) => {
        this.profileModel.set(value);
      }
    });
  }
  public updateProfileBasicInfo(model: UpdateProfileDto): Observable<ProfileModel> {
    return this.httpClient.put<ProfileModel>(this.baseUri, model);
  }
  public updateProfilePhoto(file: File) {
    const formData = new FormData()
    formData.append('formFile', file);
    return this.httpClient.put<any>(this.baseUri + '/photo', formData)
  }
  public getProfileMetrics() {
    return this.httpClient.get<ProfileMetricsView>(this.baseUri + "/metrics");
  }
}