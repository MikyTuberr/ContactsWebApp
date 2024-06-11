import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ContactsService } from '../services/contacts.service';
import { Contact } from '../models/contact';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  contacts: Contact[] = [];
  contactId: number;
  deleteId: number;
  selectedContact: Contact | null;
  newContact: Contact;

  constructor(
    private _authService: AuthService,
    private contactService: ContactsService,
  ) {
    this.contactId = 0;
    this.deleteId = 0;
    this.selectedContact = null;
    this.newContact = new Contact();
  }

  ngOnInit(): void {
    this.getContacts();
  }

  getContacts(): void {
    this.contactService.getContacts().subscribe(contacts => {
      this.contacts = contacts;
    });
  }

  getContact(id: number): void {
    this.contactService.getContact(id).subscribe(contact => {
      this.selectedContact = contact;
    });
  }

  addContact(contact: Contact): void {
    this.contactService.addContact(contact).subscribe(newContact => {
      this.contacts.push(newContact);
    });
  }

  editContact(id: number, contact: Contact): void {
    this.contactService.editContact(id, contact).subscribe(() => {
      this.getContacts();
    });
  }

  deleteContact(id: number): void {
    this.contactService.deleteContact(id).subscribe(() => {
      this.contacts = this.contacts.filter(contact => contact.id !== id);
    });
  }

  // Getters
  get authService() {
    return this._authService;
  }
}
