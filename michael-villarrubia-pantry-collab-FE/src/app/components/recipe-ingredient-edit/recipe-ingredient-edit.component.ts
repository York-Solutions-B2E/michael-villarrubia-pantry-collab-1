import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Ingredient } from 'src/app/Models/Ingredient';

@Component({
  selector: 'app-recipe-ingredient-edit',
  templateUrl: './recipe-ingredient-edit.component.html',
  styleUrls: ['./recipe-ingredient-edit.component.css'],
})
export class RecipeIngredientEditComponent implements OnInit {
  @Input() ingredient = new Ingredient(0, '', '', []);
  @Input() recipeId: number = 0;
  @Input() index: number = 0;
  @Output() removeIngredientEvent = new EventEmitter<number>();
  riIndex: number = 0;
  unitsOfMeasurement: string[] = [
    'cup',
    'tablespoon',
    'teaspoon',
    'gallon',
    'fluid ounce',
    'ounce',
    'pound',
  ];

  ngOnInit(): void {
    if (this.ingredient.recipeIngredients.length > 1) {
      let findQuantity = this.ingredient.recipeIngredients.findIndex(
        (ri) =>
          ri.ingredientId === this.ingredient.id &&
          ri.recipeId === this.recipeId
      );
      if (findQuantity) {
        this.riIndex = findQuantity;
      }
    }
  }

  delete(): void {
    console.log(this.index);
    this.removeIngredientEvent.emit(this.index);
  }
}
