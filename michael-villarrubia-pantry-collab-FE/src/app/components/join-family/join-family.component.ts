import { Component } from '@angular/core';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-join-family',
  templateUrl: './join-family.component.html',
  styleUrls: ['./join-family.component.css'],
})
export class JoinFamilyComponent {
  joinCode: string = '';

  constructor(private uiService: UiService) {}

  joinFamily(): void {
    this.uiService.joinFamily(this.joinCode);
  }

  goToCreateFamily(): void {
    this.uiService.$currentPage.next(showPage.createFamily);
  }
}
