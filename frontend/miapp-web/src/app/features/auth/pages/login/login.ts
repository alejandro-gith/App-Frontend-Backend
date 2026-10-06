import { Component } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  errorMessage = '';
  loading = false;

  loginForm;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {

    this.errorMessage = '';

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const credentials = {
      username: this.loginForm.value.username ?? '',
      password: this.loginForm.value.password ?? ''
    };

    this.loading = true;

    this.authService.login(credentials).subscribe({

      next: response => {
        this.loading = false;

        if (response.isSuccess && response.value) {
          this.router.navigate(['/catalog']);
        } else {
          this.errorMessage =
            response.error ??
            'No fue posible iniciar sesión.';
        }
      },

      error: error => {
        this.loading = false;

        this.errorMessage =
          error.error?.error ??
          'Usuario o contraseña incorrectos.';
      }

    });
  }
}