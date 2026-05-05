export class RegisterDto 
{
    public email : string = '';
    public firstName : string = '';
    public lastName : string = '';
    public password : string = '';
    public username : string = '';
    public phoneNumber :string = '';
    public birthdate : Date = new Date();
    public gender : number = 0; 
}