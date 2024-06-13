import { Component, OnInit } from '@angular/core';
import { Contact } from '../models/contact';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactsService } from '../services/contacts.service';
import { FormGroup, FormControl, Validators, FormBuilder, Form } from '@angular/forms';
import { strongPasswordValidator } from '../validators/strong.password.validator';
import { minimumAgeValidator } from '../validators/minimum.age.validator';

@Component({
  selector: 'app-contact-edit-page',
  templateUrl: './contact-edit-page.component.html',
  styleUrls: ['./contact-edit-page.component.css']
})
export class ContactEditPageComponent implements OnInit {
  contact!: Contact; // contact for edit
  id! : number; // id of contact for edit
  editForm: FormGroup; // form for contact edition

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _contactsService: ContactsService,
  ) 
  {
    // initialize form
    this.editForm = this._formBuilder.group({
      name: new FormControl<string>('', Validators.required),
      lastName: new FormControl<string>('', Validators.required),
      email: new FormControl<string>('', [Validators.required, Validators.email]),
      password: new FormControl<string>('', [Validators.required, strongPasswordValidator()]),  // pass strongPasswordValidator
      category: new FormControl<string>('', Validators.required),
      subCategory: new FormControl<string>(''),
      phoneNumber: new FormControl<string>('', Validators.required),
      birthDate: new FormControl<Date>(new Date(), [Validators.required, minimumAgeValidator(13)]) // pass minimumAgeValidator with 13 years old limit
    });
  }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      // get the id of contact for edition
      this.id = params['id'];
      // get the contact
      this._contactsService.getContact(this.id).subscribe(
        contact => {
          // assign contact
          this.contact = contact;
          // change values of form
          if (this.contact) {
            this.editForm.patchValue({
              name: this.contact.name,
              lastName: this.contact.lastName,
              email: this.contact.email,
              password: this.contact.password,
              category: this.contact.category,
              subCategory: this.contact.subCategory,
              phoneNumber: this.contact.phoneNumber,
              birthDate: this.contact.birthDate
            });
          }
        }
      );
    });
  }
  

  editContact() {
    this._contactsService.editContact(this.id, this.contact).subscribe(
      response => {
        // handle response from server
        console.log('Contact updated successfully:', response);
        this._router.navigate(['/home']);
      }
    );
  }

  
  // Getters
  get password() {
    return this.editForm.get('password');
  }

  get email() {
    return this.editForm.get('email');
  }
}
