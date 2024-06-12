import { AbstractControl, ValidatorFn } from '@angular/forms';

export function passwordMatchValidator(passwordControl: AbstractControl): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const confirmPasswordControl = control;
  
    const password = passwordControl.value;
    const confirmPassword = confirmPasswordControl.value;

    if (!confirmPassword || !password) {
      return null; // Return null if value not provided
    }
  
    return password === confirmPassword ? null : { notSame: true };
  };
}
