import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Ingredient } from 'src/app/Models/Ingredient';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-recipe-ingredient-add',
  templateUrl: './recipe-ingredient-add.component.html',
  styleUrls: ['./recipe-ingredient-add.component.css'],
})
export class RecipeIngredientAddComponent {
  ingredient = new Ingredient(0, '', '', []);
  @Input() ingredients: Ingredient[] = [];
  @Input() recipeId: number = 0;
  quantity: number = 0;

  constructor(private uiService: UiService) {}

  addIngredient(): void {
    this.uiService.addIngredient(this.quantity, this.ingredient, this.recipeId);
  }
}
