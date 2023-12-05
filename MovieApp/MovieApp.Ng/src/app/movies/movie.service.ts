import { Injectable } from '@angular/core';
import { Movie } from '../models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor() { }

  getMoviePage(pageSize: number, pageNo: number) : Movie[]
  {
    return [
      {id:1, title: "Starwars", info:"Starwars (George Lucas)"},
      {id:2, title: "Blade Runner", info:"Blade Runner (Ridley Scott)"}
    ];
  }
}
