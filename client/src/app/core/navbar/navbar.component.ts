import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
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
