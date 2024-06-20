import { Role } from "../enums/role";

export class RegisterationDTO
{
  constructor(
    public fullName: string,
    public email: string,
    public password:string,
    public confirmPassword:string,
    public role:Role,
    public districtId:number ,
    public jobId:number
    ) { }
}
