export class Movie {
    id: number;
    releaseDate: Date;
    title?: string;
    description?: string;
  
    constructor(id: number, releaseDate: Date, title?: string, description?: string) {
      this.id = id;
      this.releaseDate = releaseDate;
      this.title = title;
      this.description = description;
    }
  }
  