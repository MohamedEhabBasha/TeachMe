import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [BsDropdownModule, RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  accountService = inject(AccountService);
  private router = inject(Router)
  
  getStatred() {
    this.router.navigateByUrl('/register');
  }
  logout() {
    this.accountService.logout();
  }
}
