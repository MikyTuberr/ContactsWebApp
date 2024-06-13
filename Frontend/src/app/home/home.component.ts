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
  contacts: Contact[] = []; // list of contacts

  constructor(
    private _authService: AuthService,
    private _contactService: ContactsService,
  ) {}

  ngOnInit(): void {
    this.getContacts();
  }

  getContacts(): void {
    // get the list of contacts
    this._contactService.getContacts().subscribe(
      contacts => {
        this.contacts = contacts;
      }
    );
  }

  deleteContact(id?: number): void {
    // delete contact
    this._contactService.deleteContact(id).subscribe(
      response => {
        // handle response from server
        console.log("Succesfully deleted contact!", response);
        this.contacts = this.contacts.filter(contact => contact.id !== id);
      }
    );
  }

  // Getters
  get authService() {
    return this._authService;
  }
}
