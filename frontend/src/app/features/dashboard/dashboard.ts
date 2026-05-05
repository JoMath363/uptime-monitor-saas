import { Component, signal } from '@angular/core';

@Component({
  selector: 'dashboard',
  templateUrl: `./dashboard.html`,
})
export class Dashboard {
  protected readonly title = signal('frontend');
}
