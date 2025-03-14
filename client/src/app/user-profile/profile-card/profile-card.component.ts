import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AccountService } from '../../_services/account.service';
import { UserprofileService } from '../../_services/userprofile.service';

@Component({
  selector: 'app-profile-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './profile-card.component.html',
  styleUrl: './profile-card.component.css'
})
export class ProfileCardComponent {
  accountService = inject(AccountService);
  profileService = inject(UserprofileService);
}
