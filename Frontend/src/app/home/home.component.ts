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

  constructor(
    private _authService: AuthService,
    private _contactService: ContactsService,
  ) {}

  ngOnInit(): void {
    this.getContacts();
  }

  getContacts(): void {
    this._contactService.getContacts().subscribe(
      contacts => {
        this.contacts = contacts;
      }
    );
  }

  deleteContact(id?: number): void {
    this._contactService.deleteContact(id).subscribe(
      response => {
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
