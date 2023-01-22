import { Pantry } from './Pantry';
import { User } from './User';

export class Family {
  constructor(
    public id: number,
    public name: string,
    public code: string,
    public recipes: any[],
    public sentInvitations: any[],
    public receivedInvitations: any[]
  ) {}
}
