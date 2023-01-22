import { PantryItem } from './PantryItem';

export class Pantry {
  constructor(
    public id: number,
    public items: PantryItem[],
    public familyId: number
  ) {}
}
