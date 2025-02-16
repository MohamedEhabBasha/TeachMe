import { Component, inject, OnInit, signal } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { AccountService } from '../../../_services/account.service';
import { Router } from '@angular/router';
import { TextInputComponent } from '../../../_forms/text-input/text-input.component';
import { WheelComponent } from "./wheel/wheel.component";
import { debounceTime } from 'rxjs';

export function passwordValidator() {
  return (control: AbstractControl) => {
    const value = control.value as string;
    if (!value) {
      return null; // Return null if there's no value (optional field)
    }

    const hasUpperCase = /[A-Z]/.test(value);
    const hasLowerCase = /[a-z]/.test(value);
    const hasNumber = /[0-9]/.test(value);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(value);

    const isValid = hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar;

    return isValid ? null : { passwordStrength: true }; // Return an error if invalid
  };
}
function equalValues(controlName1: string, controlName2: string){
  return (control: AbstractControl) => {
    const val1 = control.get(controlName1)?.value;
    const val2 = control.get(controlName2)?.value;
  
    if (val1 === val2)
      return null;
  
    return {valuesNotEquals: true};
  }
}

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, WheelComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  registerForm: FormGroup = new FormGroup({});
  serverValidationErrors = signal<string[] | undefined>(undefined);

  formState = signal<'WELCOME' | 'VALID' | 'INVALID'>('WELCOME');

  ngOnInit(): void {
      this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      userName: ['', Validators.required],
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(6), passwordValidator()]],
        confirmPassword: ['', [Validators.required]],
      },{
        validators: equalValues('password', 'confirmPassword'),
      }),
      role: ['Student', [Validators.required]]
    });
    this.registerForm.controls['passwords'].valueChanges.pipe(debounceTime(500)).subscribe({
      next: () => this.registerForm.controls['passwords'].updateValueAndValidity()
    });

/*     setTimeout(() => {
      this.formState.set('WELCOME');
    }, 500); */

    this.registerForm.statusChanges.pipe(debounceTime(500)).subscribe((status) => {
      this.formState.set(this.registerForm.valid && !this.serverValidationErrors() ? 'VALID' : 'INVALID');
    });
  }

  onSubmit(){
    if (this.registerForm.invalid)
      return;
    
    let model = this.registerModel(this.registerForm.value);
    console.log(model)
    this.accountService.register(model).subscribe({
      next: _ => this.router.navigateByUrl('/') ,
      error: error => {
        if(error.error.detail)
          this.serverValidationErrors.set(error.error.detail);
        else
          this.serverValidationErrors.set(["Server Error!!"]);
      }
    });
  }
  onRest(){
    this.registerForm.reset();
  }
  login() {
    this.router.navigateByUrl('/login');
  }
  get checkPasswords() {
    return this.registerForm.get('passwords')?.touched &&
    this.registerForm.get('passwords')?.dirty && 
    this.registerForm.get('passwords')?.invalid;
  }
  private registerModel(form: any) {
    let model = {
      registerDto: {
        name: form.name,
        userName: form.userName,
        password: form.passwords.password,
        role: form.role
      }
    }

    return model;
  }
}
