export class RecipeIngredient {
  constructor(
    public quantity: number,
    public recipeId: number,
    public ingredientId: number
  ) {}
}
