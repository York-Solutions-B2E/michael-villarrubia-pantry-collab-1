import { RecipeIngredient } from './RecipeIngredient';

export class Ingredient {
  constructor(
    public id: number,
    public name: string,
    public unitOfMeasurement: string,
    public recipeIngredients: RecipeIngredient[]
  ) {}
}
