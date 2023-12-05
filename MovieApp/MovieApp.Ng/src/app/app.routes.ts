import { Routes } from '@angular/router';
import { Component } from '@angular/core';
import { WelcomeComponent } from './welcome.component';
import { MovieListComponent } from './movies/movie-list.component';

export const routes: Routes = [
  { path: '', component: WelcomeComponent},
  { path: 'welcome', component: WelcomeComponent},
  { path: 'movies', component: MovieListComponent},
  { path: 'movie/:id', component: MovieListComponent},
];
