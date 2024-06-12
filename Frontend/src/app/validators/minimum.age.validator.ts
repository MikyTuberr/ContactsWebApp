import { AbstractControl, ValidatorFn } from '@angular/forms';

// Custom validator function to check if a person is at least 13 years old
export function minimumAgeValidator(minimumAge: number): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    if (control.value) {
      const birthDate = new Date(control.value);
      const ageDiffMs = Date.now() - birthDate.getTime();
      const ageDate = new Date(ageDiffMs);
      const personAge = Math.abs(ageDate.getUTCFullYear() - 1970);

      if (personAge < minimumAge) {
        return { minimumAge: { requiredAge: minimumAge, actualAge: personAge } };
      }
    }
    return null;
  };
}