export class RegistrationDTO {
    constructor(
        public Fullname:string,
        public Email:string,
        public Password:string,
        public ConfirmPassword:string,
        public Role:string,
        public DistrictId:number|null,
        public JobId:number|null
    ) {}
}
