import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-star-rate',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './star-rate.component.html',
  styleUrl: './star-rate.component.css'
})
export class StarRateComponent {
  @Input() rate: number = 0;
  @Output() rateChange: EventEmitter<number> = new EventEmitter<number>();
  stars: boolean[] = Array(5).fill(false);

  ngOnChanges(changes: SimpleChanges) {
    if (changes['rate']) {
      this.updateStars();
    }
  }

  setRate(newRate: number): void {
    this.rate = newRate;
    this.rateChange.emit(this.rate); // Emit the rate change event
    this.updateStars();
  }

  updateStars() {
    this.stars = this.stars.map((_, i) => i < this.rate);
  }
}
