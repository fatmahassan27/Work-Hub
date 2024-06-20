import { Role } from "../enums/role";

export interface UserInfo {
    id: number;
    name: string;
    role: Role; 
    email: string; 
    districtId: string, 
    jobId: string
}
