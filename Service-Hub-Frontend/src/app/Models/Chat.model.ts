export class ChatMessage
{
    constructor( public Id: number ,
        public SenderId:number ,
        public ReceiverId :number,
        public Message :string   ,
        public IsSeen: boolean ,
        public createdDate : Date )
    {}
}