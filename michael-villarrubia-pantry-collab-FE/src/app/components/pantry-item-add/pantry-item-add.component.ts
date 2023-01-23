import { Component } from '@angular/core';
import { PantryItem } from 'src/app/Models/PantryItem';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-pantry-item-add',
  templateUrl: './pantry-item-add.component.html',
  styleUrls: ['./pantry-item-add.component.css'],
})
export class PantryItemAddComponent {
  item = new PantryItem(0, '', '', 0, 0, 0, 0);

  constructor(private uiService: UiService) {}

  addItem(): void {
    this.uiService.addItemToPantry(this.item);
    this.uiService.$currentPage.next(showPage.pantry);
  }
}
