export class Rate {
    constructor(
      public Id: number,
      public Value: number,
      public Review: string,
      public FromUserId: number,
      public ToUserId: number
    ) {}
  }
  