import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { ContactsService } from '../services/contacts.service';
import { Contact } from '../models/contact';
import { strongPasswordValidator } from '../validators/strong.password.validator';
import { minimumAgeValidator } from '../validators/minimum.age.validator';

@Component({
  selector: 'app-contact-add-page',
  templateUrl: './contact-add-page.component.html',
  styleUrls: ['./contact-add-page.component.css'] // Zmieniłem styleUrl na styleUrls
})
export class ContactAddPageComponent {
  newContact: Contact;
  addForm: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _contactsService: ContactsService
  ) {
    this.newContact = new Contact();

    this.addForm = this._formBuilder.group({
      name: new FormControl<string>('', Validators.required),
      lastName: new FormControl<string>('', Validators.required),
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      password: new FormControl<string>('', [Validators.required, strongPasswordValidator()]),
      category: new FormControl<string>('', Validators.required),
      subCategory: new FormControl<string>(''),
      phoneNumber: new FormControl<string>('', Validators.required),
      birthDate: new FormControl<Date>(new Date(), [Validators.required, minimumAgeValidator(13)])
    });
  }

  addContact() {
    this._contactsService.addContact(this.newContact).subscribe(
      response => {
        console.log('Contact added successfully:', response);
        this._router.navigate(['/home']);
      }
    );
  }

  // Getters
  get password() {
    return this.addForm.get('password');
  }

  get email() {
    return this.addForm.get('email');
  }

}
