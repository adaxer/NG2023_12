import { Component, OnInit } from '@angular/core';
import { MovieService } from './movie.service';
import { MovieInfo } from '../models/movie-info';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { RouterModule } from '@angular/router';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-movie-list',
  standalone: true,
  imports: [MatListModule,MatProgressSpinnerModule,RouterModule],
  templateUrl: './movie-list.component.html',
  styles: ``
})
export class MovieListComponent implements OnInit {
  movies?: MovieInfo[];

  constructor(private service: MovieService, private signalR: SignalRService){
  }

  ngOnInit(): void {
    this.service.getMoviePage(10, 0).subscribe(page => {
      console.log(page);
      this.movies = page.data;
    });

    this.signalR.moviesChanged.subscribe(()=>{
      this.service.getMoviePage(1,1).subscribe(p=> {
        console.log(`Now there are ${p.totalCount} Movies in the database`);
      })
    })
  }
}
