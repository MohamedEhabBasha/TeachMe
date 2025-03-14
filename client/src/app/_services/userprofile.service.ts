import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Profile } from '../_models/profile';

@Injectable({
  providedIn: 'root'
})
export class UserprofileService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl + 'userProfile/';
  profile = signal<Profile | null>(null);

  setUserProfile(profile: Profile) {
    this.profile.set(profile);
  }

  getUserProfile(id: string) {
    return this.http.get<any>(this.baseUrl + 'userProfileById', {
      params: {id}
    });
  }

  UpdateUserProfileFollowStatus(model: any) {
    return this.http.put(this.baseUrl + 'updateProfileFollowStatus', model);
  }

  updateUserProfile(model: any) {
    return this.http.put(this.baseUrl, model);
  }
}
