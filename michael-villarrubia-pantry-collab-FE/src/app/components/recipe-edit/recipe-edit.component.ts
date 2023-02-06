import { Component, Input } from '@angular/core';
import { Ingredient } from 'src/app/Models/Ingredient';
import { Recipe } from 'src/app/Models/Recipe';

@Component({
  selector: 'app-recipe-edit',
  templateUrl: './recipe-edit.component.html',
  styleUrls: ['./recipe-edit.component.css'],
})
export class RecipeEditComponent {
  @Input() recipe = new Recipe(0, '', '', '', '', []);

  getQuantity(ingredient: Ingredient): number {
    let quantity = ingredient.recipeIngredients.find(
      (ri) =>
        ri.ingredientId === ingredient.id && ri.recipeId === this.recipe.id
    )?.quantity;

    return quantity ? quantity : 0;
  }

  removeIngredient(index: number) {
    this.recipe.ingredients.splice(index, 1);
  }

  addIngredient(): void {
    this.recipe.ingredients.push(new Ingredient(12941, '', '', []));
  }
}
