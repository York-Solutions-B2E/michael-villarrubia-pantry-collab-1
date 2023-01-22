import { Component } from '@angular/core';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-create-family',
  templateUrl: './create-family.component.html',
  styleUrls: ['./create-family.component.css'],
})
export class CreateFamilyComponent {
  constructor(private uiService: UiService) {}

  goToJoinFamily(): void {
    this.uiService.$currentPage.next(showPage.joinFamily);
  }
}
