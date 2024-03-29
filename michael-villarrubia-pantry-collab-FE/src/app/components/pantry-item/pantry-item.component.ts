import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Ingredient } from 'src/app/Models/Ingredient';
import { PantryItem } from 'src/app/Models/PantryItem';
import { Recipe } from 'src/app/Models/Recipe';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-pantry-item',
  templateUrl: './pantry-item.component.html',
  styleUrls: ['./pantry-item.component.css'],
})
export class PantryItemComponent implements OnInit, OnDestroy {
  @Input() pantryItem: PantryItem = new PantryItem(0, '', '', 0, 0, '', 0, 0);
  @Input() familyId: number = 0;
  ingredients: Ingredient[] | undefined;
  recipes: Recipe[] = [];
  edit: boolean = false;
  unitsOfMeasurement: string[] = [
    'cup',
    'tablespoon',
    'teaspoon',
    'gallon',
    'fluid ounce',
    'ounce',
    'pound',
  ];

  $recipesSub = new Subscription();

  constructor(private uiService: UiService) {}

  ngOnInit(): void {
    this.$recipesSub = this.uiService.$recipes.subscribe((recipes) => {
      this.recipes = recipes;
      let recipeExists = recipes.find((r) => r.name === this.pantryItem.name);
      if (recipeExists) {
        this.ingredients = recipeExists.ingredients;
      }
    });
  }

  ngOnDestroy(): void {
    this.$recipesSub.unsubscribe();
  }

  deleteItem(): void {
    this.uiService.deletePantryItem(
      this.pantryItem.pantryId,
      this.pantryItem.id
    );
  }

  saveChanges(): void {
    this.uiService.editPantryItem(
      this.pantryItem.pantryId,
      this.pantryItem.id,
      this.pantryItem
    );
    this.edit = false;
  }

  cancel(): void {
    this.edit = false;
  }
}
