import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { strongPasswordValidator } from '../validators/strong.password.validator'; 
import { passwordMatchValidator } from '../validators/password.match.validator'
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup; // form for registration
  registrationError: string | null = null; // error string

  constructor(
    private formBuilder: FormBuilder, 
    private authService: AuthService,
    private router: Router 
  ) {
    // initialization of password control
    let passwordControl: FormControl = new FormControl<string>('', [Validators.required, strongPasswordValidator()]);

    // initialize registration form
    this.registerForm = this.formBuilder.group({
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      password: passwordControl,
      confirmPassword: new FormControl<string>('', [Validators.required, passwordMatchValidator(passwordControl)]), // pass password control as arg
      firstName: new FormControl<string>('', Validators.required),
      lastName: new FormControl<string>('', Validators.required)
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const user = this.registerForm.value;
      delete user.confirmPassword; // Remove the confirmPassword field before sending the data to the server

      this.authService.register(user).subscribe(
        () => {
          // If registration is successful, we can redirect the user or display a success message
          console.log('Registration successful!');
          this.router.navigate(['/home']);
        },
        error => {
          // If registration fails, handle the error and display an appropriate message to the user
          console.error('Registration error:', error);
          this.registrationError = error.message || 'An error occurred during registration. Please try again later.';
        }
      );
    }
  }

  // Getters
  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }

  get firstName() {
    return this.registerForm.get('firstName');
  }

  get lastName() {
    return this.registerForm.get('lastName');
  }
}
