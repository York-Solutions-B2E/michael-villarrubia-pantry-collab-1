import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Recipe } from 'src/app/Models/Recipe';
import { RedditPost } from 'src/app/Models/redditPost';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css'],
})
export class RecipeComponent {
  @Input() recipe = new Recipe(0, '', '', '', '', []);
  @Input() redditPost = new RedditPost('', '', '', true);
  edit: boolean = false;

  constructor(private uiService: UiService) {}

  deleteRecipe() {
    this.uiService.deleteRecipe(this.recipe.id);
  }

  editRecipe() {
    this.edit = true;
  }
}
