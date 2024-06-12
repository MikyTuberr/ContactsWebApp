import { Component, OnInit } from '@angular/core';
import { Contact } from '../models/contact';
import { ActivatedRoute } from '@angular/router';
import { ContactsService } from '../services/contacts.service';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.component.html',
  styleUrls: ['./contact-page.component.css']
})
export class ContactPageComponent implements OnInit {
  contact!: Contact;
  id! : number;

  constructor(
    private _route: ActivatedRoute,
    private _contactsService: ContactsService
  ) {}

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      this.id = params['id'];
      this._contactsService.getContact(this.id).subscribe(
        contact => {
          this.contact = contact;
        }
      );
    });
  }
}
