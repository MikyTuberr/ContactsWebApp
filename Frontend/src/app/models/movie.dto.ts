export class MovieDto {
    id? : number;
    releaseDate: Date;
    title?: string;
    description?: string;
  
    constructor(releaseDate: Date, id? : number,  title?: string, description?: string) {
      this.id = id;
      this.releaseDate = releaseDate;
      this.title = title;
      this.description = description;
    }
  }
  