import { Component, OnInit } from '@angular/core';
import { faMoon, faSun } from '@fortawesome/free-solid-svg-icons';

enum Theme {
  Light,
  Dark
}

@Component({
  selector: 'app-theme-selector',
  templateUrl: './theme-selector.component.html',
  styleUrls: ['./theme-selector.component.scss']
})
export class ThemeSelectorComponent implements OnInit {

  public faThemeIcon = faMoon;
  public theme: Theme = Theme.Light;

  public get themeText(): string {
    switch (this.theme) {
      case Theme.Light:
        return "light";
      case Theme.Dark:
        return "dark";
    }
  }

  public ngOnInit(): void {
    const themeText = localStorage.getItem('theme');
    if (themeText) {
      const theme = themeText === 'light' ? Theme.Light : Theme.Dark;
      this.setTheme(theme);
    }
  }

  public setTheme(theme: Theme): void {
    if (theme === Theme.Light) {
      document.documentElement.classList.remove('dark-mode');
      document.querySelectorAll('.inverted').forEach(result => {
        result.classList.remove('invert');
      });
      localStorage.setItem('theme', 'light');
      this.faThemeIcon = faMoon;
      this.theme = Theme.Light;
    }
    else {
      document.documentElement.classList.add('dark-mode');
      document.querySelectorAll('.inverted').forEach(result => {
        result.classList.add('invert');
      });
      localStorage.setItem('theme', 'dark');
      this.faThemeIcon = faSun;
      this.theme = Theme.Dark;
    }
  }

  public onThemeToggle(): void {
    switch (this.theme) {
      case Theme.Light:
        this.setTheme(Theme.Dark);
        break;
      case Theme.Dark:
        this.setTheme(Theme.Light);
        break;
    }
  }
}
