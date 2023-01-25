import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Ingredient } from 'src/app/Models/Ingredient';
import { Recipe } from 'src/app/Models/Recipe';
import { RecipeIngredient } from 'src/app/Models/RecipeIngredient';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-recipe-ingredient-add',
  templateUrl: './recipe-ingredient-add.component.html',
  styleUrls: ['./recipe-ingredient-add.component.css'],
})
export class RecipeIngredientAddComponent {
  ingredient = new Ingredient(0, '', '', []);
  @Input() ingredients: Ingredient[] = [];
  @Input() recipe = new Recipe(0, '', '', '', '', []);
  @Output() recipeEvent = new EventEmitter<Recipe>();
  ingredientName = '';
  unitOfMeasurement: string = '';
  quantity: number = 0;

  constructor(private uiService: UiService) {}

  addIngredient(): void {
    this.ingredient.recipeIngredients.push(
      new RecipeIngredient(this.quantity, 0, 0)
    );
    this.recipe.ingredients.push(this.ingredient);
    console.log(this.recipe);
    this.recipeEvent.emit(this.recipe);
    this.ingredient = new Ingredient(0, '', '', []);
  }
}
