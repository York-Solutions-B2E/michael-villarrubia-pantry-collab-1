import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
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
  @Input() redditPost = new RedditPost('', '', '', '', '', 0, true);
  @Output() refresh = new EventEmitter();
  edit: boolean = false;

  constructor(private uiService: UiService) {}

  deleteRecipe() {
    this.uiService.deleteRecipe(this.recipe.id);
  }

  editRecipe() {
    this.edit = true;
  }

  formatRedditPost(): string[] {
    let formatted: string[] = this.redditPost.instructions.split('\n');
    return formatted;
  }

  isBold(line: string): boolean | string {
    if (line.includes('**')) {
      return line.replaceAll('*', '');
    } else {
      return false;
    }
  }

  refreshRecipes(): void {
    this.edit = false;
    this.refresh.emit();
  }
}
