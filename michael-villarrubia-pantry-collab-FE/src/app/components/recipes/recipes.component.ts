import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Recipe } from 'src/app/Models/Recipe';
import { RedditPost } from 'src/app/Models/redditPost';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.css'],
})
export class RecipesComponent implements OnInit, OnDestroy {
  recipes: Recipe[] = [];
  ingredients: string[] = [];
  index: number = 0;
  redditPost = new RedditPost('', '', '', true);
  ingredientsSelected: string[] = [];
  recipeSelected = new Recipe(0, '', '', '', '', []);

  $recipesSub = new Subscription();
  $redditPostSub = new Subscription();
  $ingredientsSub = new Subscription();

  constructor(private uiService: UiService) {}

  ngOnInit(): void {
    this.recipeSelected;
    this.$recipesSub = this.uiService.$recipes.subscribe((recipes) => {
      this.recipes = recipes;
      this.recipeSelected = recipes[0];
    });
    this.$redditPostSub = this.uiService.$redditPost.subscribe(
      (redditPost) => (this.redditPost = redditPost)
    );
    this.$ingredientsSub = this.uiService.$ingredients.subscribe(
      (ingredients) => (this.ingredients = ingredients)
    );
  }

  ngOnDestroy(): void {
    this.$recipesSub.unsubscribe();
    this.$redditPostSub.unsubscribe();
    this.$ingredientsSub.unsubscribe();
  }

  addRecipe(): void {
    this.uiService.$currentPage.next(showPage.recipeAdd);
  }

  isSelected(ingredient: string): void {
    if (!this.ingredientsSelected.includes(ingredient)) {
      this.ingredientsSelected.push(ingredient);
      console.log(this.ingredientsSelected);
      return;
    }
    this.ingredientsSelected = this.ingredientsSelected.filter(
      (i) => i !== ingredient
    );
    console.log(this.ingredientsSelected);
  }

  selectRecipe(recipe: Recipe): void {
    this.recipeSelected = recipe;
    this.uiService.getRedditTopSearch(recipe.name);
  }

  isRecipeSelected(recipe: Recipe): boolean {
    return recipe.id === this.recipeSelected.id;
  }

  getSearchString(): string {
    let searchString = '';
    this.ingredientsSelected.forEach((i) => {
      searchString = searchString.concat(i + ' ');
    });
    return `https://www.reddit.com/r/recipes/search?q=${searchString}&restrict_sr=on&type=comment`;
  }
}
