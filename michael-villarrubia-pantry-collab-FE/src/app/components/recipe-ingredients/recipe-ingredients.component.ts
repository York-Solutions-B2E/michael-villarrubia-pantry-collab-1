import { Component, Input, OnInit } from '@angular/core';
import { Ingredient } from 'src/app/Models/Ingredient';

@Component({
  selector: 'app-recipe-ingredients',
  templateUrl: './recipe-ingredients.component.html',
  styleUrls: ['./recipe-ingredients.component.css'],
})
export class RecipeIngredientsComponent implements OnInit {
  @Input() ingredient = new Ingredient(0, '', '', []);
  @Input() recipeId: number = 0;
  quantity: number | undefined;

  ngOnInit(): void {
    this.getQuantity();
  }

  getQuantity(): void {
    this.quantity = this.ingredient.recipeIngredients.find(
      (ri) => ri.recipeId === this.recipeId
    )?.quantity;
  }
}
