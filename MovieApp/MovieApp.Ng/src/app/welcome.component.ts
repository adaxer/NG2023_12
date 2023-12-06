import { Component, ViewChild } from '@angular/core';
import { DecoratePipe } from './pipes/decorate.pipe';
import { Weekdays } from './models/weekdays';
import { LedComponent } from '../../projects/shared-controls/src/lib/led.component';

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

  @ViewChild('led')
  led?: LedComponent;

  constructor() {
    console.log(Weekdays.Monday, Weekdays[Weekdays.TuesDay]);
  }

  componentChanged(newColor: string) {
    console.log(newColor);
    console.log(this.led);
  }
}
