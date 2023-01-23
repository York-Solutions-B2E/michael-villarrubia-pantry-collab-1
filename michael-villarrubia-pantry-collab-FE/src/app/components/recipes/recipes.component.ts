import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Recipe } from 'src/app/Models/Recipe';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.css'],
})
export class RecipesComponent implements OnInit, OnDestroy {
  recipes: Recipe[] = [];
  $recipesSub = new Subscription();

  constructor(private uiService: UiService) {}

  ngOnInit(): void {
    this.$recipesSub = this.uiService.$recipes.subscribe(
      (recipes) => (this.recipes = recipes)
    );
  }

  ngOnDestroy(): void {
    this.$recipesSub.unsubscribe();
  }
}
