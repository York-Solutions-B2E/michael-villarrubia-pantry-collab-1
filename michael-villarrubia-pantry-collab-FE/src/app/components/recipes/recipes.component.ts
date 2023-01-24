import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Recipe } from 'src/app/Models/Recipe';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.css'],
})
export class RecipesComponent implements OnInit, OnDestroy {
  recipes: Recipe[] = [];
  $recipesSub = new Subscription();
  index: number = 0;

  constructor(private uiService: UiService) {}

  ngOnInit(): void {
    this.$recipesSub = this.uiService.$recipes.subscribe(
      (recipes) => (this.recipes = recipes)
    );
  }

  ngOnDestroy(): void {
    this.$recipesSub.unsubscribe();
  }

  addRecipe(): void {
    this.uiService.$currentPage.next(showPage.recipeAdd);
  }

  nextRecipe(): void {
    if (this.index < this.recipes.length - 1) this.index++;
  }

  previousRecipe(): void {
    if (this.index > 0) this.index--;
  }
}
