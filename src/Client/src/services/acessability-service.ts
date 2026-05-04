import { Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AcessabilityService {
  constructor(private router : Router) {
    
  }
  public isDarkMode = signal(false);
  public toggleTheme(): void {
    if (this.isDarkMode() == true) {
      document.documentElement.setAttribute('data-theme', 'light');
      this.isDarkMode.set(false);
      localStorage.setItem('data-theme', 'light');
    }
    else {
      document.documentElement.setAttribute('data-theme', 'dark');
      this.isDarkMode.set(true);
      localStorage.setItem('data-theme', 'dark');
    }
  }
}
