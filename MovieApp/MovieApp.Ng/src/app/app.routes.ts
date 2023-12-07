import { Routes } from '@angular/router';
import { Component } from '@angular/core';
import { WelcomeComponent } from './welcome.component';
import { MovieListComponent } from './movies/movie-list.component';
import { MovieDetailsComponent } from './movies/movie-details.component';
import { LoginComponent } from './user/login.component';

export const routes: Routes = [
  { path: '', component: WelcomeComponent},
  { path: 'welcome', component: WelcomeComponent},
  { path: 'movies', component: MovieListComponent},
  { path: 'movie/:id', component: MovieDetailsComponent},
  { path: 'login', component: LoginComponent},
];
