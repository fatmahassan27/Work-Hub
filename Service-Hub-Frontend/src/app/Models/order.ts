import { OrderStatus } from "../enums/order-status";

export class Order {
    constructor(
        public id:number,
        public userId:number,
        public workerId:number,
        public status: OrderStatus,
        public createdDate: Date
    ){}
}
