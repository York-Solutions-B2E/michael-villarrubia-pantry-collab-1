import { Ingredient } from './Ingredient';

export class Recipe {
  constructor(
    public id: number,
    public name: string,
    public image: string,
    public instructions: string,
    public creator: string,
    public ingredients: Ingredient[]
  ) {}
}
