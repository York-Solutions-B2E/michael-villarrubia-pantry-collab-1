import { Component, Input } from '@angular/core';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  @Input() currPage: string = '';
  familyName: string = '';
  constructor(public uiService: UiService) {
    this.uiService.$family.subscribe((family) => {
      this.familyName = family.name
        .charAt(0)
        .toUpperCase()
        .concat(family.name.slice(1, family.name.length));
    });
  }

  logout(): void {
    this.uiService.logout();
  }

  getFamilyName(): void {}
}
