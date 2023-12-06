import { Component, ViewChild } from '@angular/core';
import { DecoratePipe } from './pipes/decorate.pipe';
import { Weekdays } from './models/weekdays';
import { LedComponent } from './shared/led.component';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [DecoratePipe, LedComponent],
  templateUrl: './welcome.component.html',
  styles: ``
})
export class WelcomeComponent {
  Weekdays = Weekdays;
  ledColor = "#FFAA33";

  componentChanged(newColor: string) {
    console.log(newColor);
  }
}
