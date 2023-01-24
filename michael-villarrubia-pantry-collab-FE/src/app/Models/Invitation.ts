import { Family } from './Family';

export class Invitation {
  constructor(
    public id: number,
    public senderFamilyId: number,
    public receiverFamilyId: number,
    public accepted: boolean | null,
    public receiverFamily?: Family,
    public senderFamily?: Family
  ) {}
}
