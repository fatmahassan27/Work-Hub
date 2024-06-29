import { District } from "./District.model";
import { Job } from "./job.model";

export class Worker {
  constructor(
    public id:number,
    public userName:string,
    public email:string,
    // public jobId:number,
    // public districtId:number,
    public rating:number,
    public job:Job,
    public district:District
  ) {}
}

