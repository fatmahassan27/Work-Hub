export class Notification {
    constructor(
        public Id : number,
        public IsRead: boolean,
        public OwnerId: number,
        public Title: string,
        public Content: string,
        public CreatedDate: Date
    ){}
}
