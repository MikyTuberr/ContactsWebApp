import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  constructor(
    private _authService: AuthService,
    private router: Router
  ) {}

  
  logout(): void {
    this._authService.logout();
    this.router.navigate(['/']);
  }

  get authService() {
    return this._authService;
  }
}
