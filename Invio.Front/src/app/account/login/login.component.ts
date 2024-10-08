import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { take } from 'rxjs';
import { User } from '../../shared/models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  submitted = false;
  errorMessages: string[] = [];

  constructor(private accountService: AccountService,
    private formBuilder: FormBuilder,
    private router: Router) {
      this.accountService.user$.pipe(take(1)).subscribe({
        next: (user: User | null) => {
          if (user) {
            this.router.navigateByUrl('');
          }
        }
      })
    }

  ngOnInit(): void {
    this.initializeForm();
  }


  initializeForm(){
    this.loginForm = this.formBuilder.group({
      email:['', Validators.required],
      password:['',Validators.required]
    })
  }

  login(){
    this.submitted = true;
    this.errorMessages=[];

    if(this.loginForm.valid){
      this.accountService.login(this.loginForm.value).subscribe({
        next:(response: any)=>{
          this.router.navigateByUrl('/');
        },
        error:error=>{
          if(error.error){
            this.errorMessages = [error.error.errorMessage];
          } else {
            this.errorMessages.push("Ocorreu um erro. Por favor, tente novamente.");
          }
        }
      })
    }
  }
}
