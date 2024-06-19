export class Register
{
  constructor(
    public fullName: string,
    public email: string,
    public password:string,
    public confirmPassword:string,
    public role:number,
    public districtId:number ,
    public jobId:number,
    public createdDate:Date
  ) { }
}
