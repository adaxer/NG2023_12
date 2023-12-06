import { Injectable } from '@angular/core';
import { MovieInfo } from '../models/movie-info';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResultPage } from '../models/result-page';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
baseUrl = "https://localhost:7267";

  constructor(private http: HttpClient) { }

  getMoviePage(pageSize: number, pageNo: number): Observable<ResultPage<MovieInfo>> {
    return this.http.get<ResultPage<MovieInfo>>(`${this.baseUrl}/movies/list/${pageSize}/${pageNo}`);
  }

  getMovieDetails(id: number): MovieInfo {
    return { id: 1, description: "Starwars (George Lucas)" };
  }
}
