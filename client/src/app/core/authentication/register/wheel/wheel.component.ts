import { Component, Input, OnChanges, signal, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-wheel',
  standalone: true,
  imports: [],
  templateUrl: './wheel.component.html',
  styleUrl: './wheel.component.css'
})
export class WheelComponent implements OnChanges {
  @Input() formState: 'WELCOME' | 'VALID' | 'INVALID' = 'WELCOME';
  words = ['INVALID', 'WELCOME', 'VALID']; // Possible words
  slotPositions = signal(1);
  spinning = false;

  ngOnChanges(changes: SimpleChanges) {
    if (changes['formState']) {
      this.spinWheel();
    }
  }

  spinWheel() {
    this.spinning = true;

    setTimeout(() => {
      const targetIndex = this.words.indexOf(this.formState);
      this.slotPositions.set(targetIndex);
      this.spinning = false;
    }, 500);
  }
}
