import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{

  constructor(private accountService: AccountService){}

  ngOnInit(): void {
    this.refreshUser();
  }

  private refreshUser(){
    const jwt = this.accountService.getJWT();
    if(jwt) {

    }
  }

  title = 'Invio.Front';
}
