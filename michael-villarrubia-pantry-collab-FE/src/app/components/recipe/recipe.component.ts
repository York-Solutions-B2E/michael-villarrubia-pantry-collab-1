import { Component, Input } from '@angular/core';
import { Recipe } from 'src/app/Models/Recipe';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css'],
})
export class RecipeComponent {
  @Input() recipe: Recipe = new Recipe(0, '', '', '', '', []);

  constructor(private uiService: UiService) {}
}
