import { SharedService } from './../../shared/shared.service';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { User } from '../../shared/models/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  submitted = false;
  errorMessages: string[] = [];

  constructor(private accountService: AccountService,
    private sharedService: SharedService,
    private formBuilder: FormBuilder,
    private router: Router) {
    this.accountService.user$.pipe(take(1)).subscribe({
      next: (user: User | null) => {
        if (user) {
          this.router.navigateByUrl('/');
        }
      }
    })
  }

  ngOnInit(): void {
    this.initializeForm();
  }


  initializeForm() {
    this.registerForm = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]],
      password: ['', [Validators.required, Validators.pattern('^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$')]]
    })
  }

  register() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.registerForm.valid) {
      this.accountService.register(this.registerForm.value).subscribe({
        next: (response: any) => {
          if (response.notificacoes && Array.isArray(response.notificacoes)) {
            response.notificacoes.forEach((notification: { key: string; message: string }) => {
              this.sharedService.showNotification(true, notification.key, notification.message);
            });
          }
          // this.sharedService.showNotification(true, response.notificacoes.value.key,response.notificacoes.value.message);
          this.router.navigateByUrl('/account/login');
          console.log(response);

        },
        error: error => {
          if (error.error) {
            this.errorMessages = error.error.map((err: { message: string }) => err.message);
          }
        }
      })
    }

  }

}
