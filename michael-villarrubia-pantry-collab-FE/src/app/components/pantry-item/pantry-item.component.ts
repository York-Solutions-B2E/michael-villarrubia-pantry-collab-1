import { Component, Input } from '@angular/core';
import { PantryItem } from 'src/app/Models/PantryItem';

@Component({
  selector: 'app-pantry-item',
  templateUrl: './pantry-item.component.html',
  styleUrls: ['./pantry-item.component.css'],
})
export class PantryItemComponent {
  @Input() pantryItem: PantryItem = new PantryItem(0, '', '', 0, 0, 0, 0);
}
