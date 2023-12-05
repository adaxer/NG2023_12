import { Component, OnInit } from '@angular/core';
import { MovieService } from './movie.service';
import { ActivatedRoute } from '@angular/router';
import { Movie } from '../models/movie';

@Component({
  selector: 'app-movie-details',
  standalone: true,
  imports: [],
  templateUrl: './movie-details.component.html',
  styles: ``
})
export class MovieDetailsComponent implements OnInit {

  movie?: Movie;

  constructor(private service: MovieService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(pM => {
      let id = +pM.get("id")!;
      this.movie = this.service.getMovieDetails(id);
    });
  }
}
