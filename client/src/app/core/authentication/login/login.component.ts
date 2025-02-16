import { Component, inject, OnInit, signal } from '@angular/core';
import { AccountService } from '../../../_services/account.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TextInputComponent } from '../../../_forms/text-input/text-input.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  loginForm: FormGroup = new FormGroup({});
  serverValidationErrors = signal<string[] | undefined>(undefined);

  ngOnInit(): void {
      this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(){
    if (this.loginForm.invalid)
      return;

    let model = this.loginModel(this.loginForm.value);
    console.log(model);
    this.accountService.login(model).subscribe({
      next: _ => this.router.navigateByUrl('/') ,
      error: error => {
        if(error.error.detail)
          this.serverValidationErrors.set(error.error.detail);
        else
          this.serverValidationErrors.set(["Server Error!!"]);
      }
    });
  }

  private loginModel(form: any) {
    let model = {
      loginDto: {
        userName: form.userName,
        password: form.password,
      }
    }
    return model;
  }
}
