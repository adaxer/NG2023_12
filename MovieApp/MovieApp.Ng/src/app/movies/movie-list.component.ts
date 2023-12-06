import { Component, OnInit } from '@angular/core';
import { MovieService } from './movie.service';
import { MovieInfo } from '../models/movie-info';
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
  movies?: MovieInfo[];

  constructor(private service: MovieService){
  }

  ngOnInit(): void {
    this.service.getMoviePage(10, 0).subscribe(page => {
      console.log(page);
      this.movies = page.data;
    });
  }
}
