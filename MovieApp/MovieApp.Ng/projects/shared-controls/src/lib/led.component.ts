import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-led',
  standalone: true,
  templateUrl: './led.component.html',
  styleUrls: []
})
export class LedComponent {
  @Input() baseColor: string = '#AAAAAA';  // default to gray color
  @Input() color: string = '#4CAF50';  // default to green color
  @Output() colorChanged = new EventEmitter<string>();

  resetColor(): void {
    this.color = this.baseColor;
    this.colorChanged.emit(this.color);
  }
}
