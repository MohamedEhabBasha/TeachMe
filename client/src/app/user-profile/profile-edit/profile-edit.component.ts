import { Component, inject, signal, viewChild } from '@angular/core';
import { PhotoEditComponent } from "./photo-edit/photo-edit.component";
import { FormsModule, NgForm } from '@angular/forms';
import { UserprofileService } from '../../_services/userprofile.service';
import { Profile } from '../../_models/profile';
import { CategorySelectorComponent } from "./category-selector/category-selector.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile-edit',
  standalone: true,
  imports: [PhotoEditComponent, FormsModule, CategorySelectorComponent],
  templateUrl: './profile-edit.component.html',
  styleUrl: './profile-edit.component.css'
})
export class ProfileEditComponent{
  editFrom = viewChild<NgForm>('editForm');

  router = inject(Router);
  userProfileService = inject(UserprofileService);
  profile = signal<Profile>(this.userProfileService.profile()!);

/*   profile = signal<Profile>({
    id: '',
    categories: [],
    description: '',
    introduction: '',
    interestedUsers: 0
  }); */

  onSubmit() {
    const { interestedUsers, ...model } = this.profile();

    const parameter = {
      userProfileDto: {
        ...model
      }
    };
    
    this.userProfileService.updateUserProfile(parameter).subscribe({
      next: isSuccess => {
        this.userProfileService.setUserProfile(this.profile());
        console.log(isSuccess);
        if( isSuccess){
          this.router.navigateByUrl('userprofile');
        }
      }
    });
  }

  categoryUpdateEvent(event: string[]) {
    this.profile().categories = event.map(name => ({ name }));
    //console.log(this.profile().categories.length);
  }

  get currentSelectedCategories(): string[] {
    return this.profile().categories ? this.profile().categories.map(n => n.name) : [];
  }
}
