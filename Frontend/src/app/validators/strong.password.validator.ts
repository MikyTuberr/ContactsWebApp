import { AbstractControl, ValidatorFn } from '@angular/forms';

export function strongPasswordValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const password = control.value;

    if (!password) {
      return null; // Return null if no value is provided
    }

    // Check password length
    const hasMinimumLength = password.length >= 16;

    // Check if password contains special character, big letter and small letter
    const containsSpecialCharacters = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(password);
    const containsUpperCase = /[A-Z]+/.test(password);
    const containsLowerCase = /[a-z]+/.test(password);
    const containsNumber = /[0-9]+/.test(password);
    const isRandom = containsSpecialCharacters && containsUpperCase && containsLowerCase && containsNumber;

    // Check if password meets requirements
    const isValid = hasMinimumLength && isRandom;

    return isValid ? null : { strongPassword: true };
  };
}
