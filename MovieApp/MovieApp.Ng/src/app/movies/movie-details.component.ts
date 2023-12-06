import { Component, OnInit } from '@angular/core';
import { MovieService } from './movie.service';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinner, MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Movie } from '../models/movie';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-movie-details',
  standalone: true,
  imports: [MatCardModule, MatProgressSpinnerModule, DatePipe],
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
      this.service.getMovieDetails(id).subscribe(m => {
        console.log(m);
        this.movie = m;
      })
    });
  }
}
