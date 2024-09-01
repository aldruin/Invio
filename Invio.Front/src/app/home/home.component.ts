import { Router } from '@angular/router';
import { AccountService } from './../account/account.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  isLoggedIn = false;
  isLoading = false;

  constructor (private router: Router, public accountService: AccountService) {}

  navigateToLogin() {
    this.router.navigate(['/account/login']);
  }

  onLogin() {
    this.router.navigate(['/account/login']);
    this.isLoading = false;
  }
}
