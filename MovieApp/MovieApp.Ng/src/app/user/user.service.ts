import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
public static token?: string;
  constructor(private client: HttpClient) { }

  register(email: string, password: string) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email: email, password: password };
    return this.client.post('https://localhost:7267/register', body, { headers, observe: 'response' })
      .pipe(
        map(response => response.status === 200),
        catchError((error: HttpErrorResponse) => {
          return of(false);
        })
      );
  }

  login(email: string, password: string) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email: email, password: password };

    return this.client.post<{ accessToken: string }>('https://localhost:7267/login?useCookies=false', body, { headers, observe: 'response' })
      .pipe(
        map(response => {
          if (response.status === 200) {
            UserService.token = response.body!.accessToken;
            console.log(UserService.token);
            return true;
          }
          return false;
        }),
        catchError((error: HttpErrorResponse) => {
          // Handle error logic here if needed
          return of(false);
        })
      );
  }
}
