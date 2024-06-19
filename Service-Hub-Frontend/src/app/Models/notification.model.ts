export class Notification {
  constructor(
      public id: number,
      public isRead: boolean,
      public ownerId: number,
      public title: string,
      public content: string,
      public createdDate: Date
  ) {}
}
