import { Component, inject, OnInit } from '@angular/core';
import { ProfileCardComponent } from "./profile-card/profile-card.component";
import { ProfileSectionsComponent } from "./profile-sections/profile-sections.component";
import { AccountService } from '../_services/account.service';
import { UserprofileService } from '../_services/userprofile.service';
import { Profile } from '../_models/profile';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [ProfileCardComponent, ProfileSectionsComponent],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit{
  accountService = inject(AccountService);
  user = this.accountService.currentUser();
  userProfile = inject(UserprofileService)

  ngOnInit(): void {
      this.loadProfile();
  }

  loadProfile() {
    if (!this.user) return;
    
    this.userProfile.getUserProfile(this.user.id).subscribe({
      next: profile => {
        const userprofile: Profile = {
          id: this.user!.id,
          categories: profile.categories,
          introduction: profile.introduction,
          description: profile.description,
          interestedUsers: profile.userFollowsCount
        };

        this.userProfile.setUserProfile(userprofile);
        console.log(profile);
      }
    });
  }
}
