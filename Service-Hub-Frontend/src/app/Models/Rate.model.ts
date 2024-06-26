export class Rate {
    constructor(
      public Id: number,
      public Value: number=3,
      public Review: string,
      public FromUserId: number,
      public ToUserId: number
    ) {}
  }
  