import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { MovieService } from '../services/movie.service';
import { Movie } from '../models/movie';
import { MovieDto } from '../models/movie.dto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  movies: Movie[] = [];
  movieId: number;
  deleteId: number;
  selectedMovie: Movie | null;
  newMovie: MovieDto;

  constructor(
    private _authService: AuthService,
    private movieService: MovieService,
    private router: Router
  ) {
    this.movieId = 0;
    this.deleteId = 0;
    this.selectedMovie = null;
    this.newMovie = new MovieDto(new Date, -1, "", "");
  }

  ngOnInit(): void {
    this.getMovies();
  }

  getMovies(): void {
    this.movieService.getMovies().subscribe(movies => {
      this.movies = movies;
    });
  }

  getMovie(id: number): void {
    this.movieService.getMovie(id).subscribe(movie => {
      this.selectedMovie = movie;
    });
  }

  addMovie(movie: MovieDto): void {
    this.movieService.addMovie(movie).subscribe(newMovie => {
      this.movies.push(newMovie);
    });
  }

  editMovie(id: number, movie: Movie): void {
    this.movieService.editMovie(id, movie).subscribe(() => {
      this.getMovies();
    });
  }

  deleteMovie(id: number): void {
    this.movieService.deleteMovie(id).subscribe(() => {
      this.movies = this.movies.filter(movie => movie.id !== id);
    });
  }

  logout(): void {
    this._authService.logout();
    this.router.navigate(['/']);
  }

  // Getters
  get authService() {
    return this._authService;
  }
}
