import { OrderStatus } from "../enums/order-status";
import { Worker } from "./worker.model";

export class Order {
    constructor(
        public id:number,
        // public userId:number,
        // public workerId:number,
        public user:Worker,
        public worker:Worker,
        public status: OrderStatus,
        public createdDate: Date
    ){}
}
