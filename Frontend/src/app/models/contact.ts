export class Contact {
  id?: number; // optional for new contacts
  name: string;
  lastName: string;
  email: string;
  password: string;
  category: string;
  subCategory?: string;
  phoneNumber: string;
  birthDate: Date;

  constructor(
      name: string = "",
      lastName: string = "",
      email: string = "",
      password: string = "",
      category: string = "",
      phoneNumber: string = "",
      birthDate: Date = new Date(),
      id?: number
  ) {
      this.id = id;
      this.name = name;
      this.lastName = lastName;
      this.email = email;
      this.password = password;
      this.category = category;
      this.phoneNumber = phoneNumber;
      this.birthDate = birthDate;
  }
}