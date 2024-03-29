import { Component, OnDestroy } from '@angular/core';
import { Subscriber, Subscription } from 'rxjs';
import { Ingredient } from 'src/app/Models/Ingredient';
import { Recipe } from 'src/app/Models/Recipe';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrls: ['./recipe-add.component.css'],
})
export class RecipeAddComponent implements OnDestroy {
  recipe = new Recipe(0, '', '', '', '', []);

  step = 1;

  $createdRecipeSub = new Subscription();

  constructor(private uiService: UiService) {}

  ngOnDestroy(): void {
    this.$createdRecipeSub.unsubscribe();
  }

  removeIngredient(): void {}

  nextAction(): void {
    if (this.step === 3) {
      this.uiService.addRecipe(this.recipe);
      this.uiService.getIngredients(this.uiService.$family.value.id);
      this.uiService.$currentPage.next(showPage.recipes);
      return;
    }
    if (this.recipe.name.length < 2 && this.step == 1) {
      this.uiService.openSnackBar('Recipe name must be at least 2 characters');
      return;
    }
    if (this.recipe.ingredients.length == 0 && this.step == 2) {
      this.uiService.openSnackBar('Recipe must have at least one ingredient');
      return;
    }

    this.step++;
  }
}
