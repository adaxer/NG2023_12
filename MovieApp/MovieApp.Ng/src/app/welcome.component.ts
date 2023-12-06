import { Component } from '@angular/core';
import { DecoratePipe } from './pipes/decorate.pipe';
import { Weekdays } from './models/weekdays';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [DecoratePipe],
  templateUrl: './welcome.component.html',
  styles: ``
})
export class WelcomeComponent {
Weekdays = Weekdays;
}
