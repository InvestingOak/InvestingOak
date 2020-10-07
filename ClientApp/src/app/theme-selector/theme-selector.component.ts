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
    const theme = JSON.parse(localStorage.getItem('theme'));
    if (theme) {
      this.setTheme(theme);
    }
  }

  public setTheme(theme: Theme): void {
    if (theme === Theme.Light) {
      document.documentElement.classList.remove('dark-mode');
      document.querySelectorAll('.inverted').forEach(result => {
        result.classList.remove('invert');
      });
      this.faThemeIcon = faMoon;
    }
    else {
      document.documentElement.classList.add('dark-mode');
      document.querySelectorAll('.inverted').forEach(result => {
        result.classList.add('invert');
      });
      this.faThemeIcon = faSun;
    }

    localStorage.setItem('theme', JSON.stringify(theme));
    this.theme = theme;
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
