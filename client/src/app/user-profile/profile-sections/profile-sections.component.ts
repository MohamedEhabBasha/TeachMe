import { Component, inject } from '@angular/core';
import { TabDirective, TabsModule, TabsetComponent } from 'ngx-bootstrap/tabs';
import { UserprofileService } from '../../_services/userprofile.service';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-profile-sections',
  standalone: true,
  imports: [TabsModule],
  templateUrl: './profile-sections.component.html',
  styleUrl: './profile-sections.component.css'
})
export class ProfileSectionsComponent {
  accountService = inject(AccountService);
  profile = inject(UserprofileService);
}
