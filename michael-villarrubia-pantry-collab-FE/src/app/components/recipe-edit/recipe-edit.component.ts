import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Ingredient } from 'src/app/Models/Ingredient';
import { Recipe } from 'src/app/Models/Recipe';
import { RecipeIngredient } from 'src/app/Models/RecipeIngredient';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-recipe-edit',
  templateUrl: './recipe-edit.component.html',
  styleUrls: ['./recipe-edit.component.css'],
})
export class RecipeEditComponent {
  @Input() recipe = new Recipe(0, '', '', '', '', []);
  @Output() doneEditingEvent = new EventEmitter();

  constructor(private uiService: UiService) {}

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
    this.recipe.ingredients.push(
      new Ingredient(0, '', '', [new RecipeIngredient(0, 0, 0)])
    );
  }

  saveChanges(): void {
    console.log(this.recipe);
    this.uiService.editRecipe(this.recipe.id, this.recipe);
    this.doneEditingEvent.emit();
  }

  cancel(): void {
    this.doneEditingEvent.emit();
  }
}
