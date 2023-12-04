import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  template: `
    <h1>Welcome to {{title}}!</h1>
    <button (click)="sayHello()">Click me!</button>
    <router-outlet></router-outlet>
  `,
  styles: [],
})
export class AppComponent {
  title = 'HelloNg';

  sayHello() {
    console.log("Hello from ", this);
  }

}
