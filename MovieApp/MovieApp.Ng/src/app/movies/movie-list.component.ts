import { Component, OnInit } from '@angular/core';
import { MovieService } from './movie.service';
import { Movie } from '../models/movie';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-movie-list',
  standalone: true,
  imports: [MatListModule,MatProgressSpinnerModule,RouterModule],
  templateUrl: './movie-list.component.html',
  styles: ``
})
export class MovieListComponent implements OnInit {
  movies?: Movie[];

  constructor(private service: MovieService){
  }

  ngOnInit(): void {
    this.movies = this.service.getMoviePage(10, 0);
  }
}
