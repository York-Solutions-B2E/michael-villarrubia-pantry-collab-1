import { Pantry } from './Pantry';

export class PantryItem {
  constructor(
    public id: number,
    public name: string,
    public image: string,
    public weight: number,
    public calories: number,
    public quantityInPantry: number,
    public pantryId: number
  ) {}
}
