import { Gender } from "../enums/gender";

export class UpdateProfileDto {
    public username : string = '';
    public firstName : string = '';
    public lastName :string = '';
    public phoneNumber : string = '';
    public birthdate : string = ''
    public biography : string = '';
    public gender : Gender = Gender.other;
}
