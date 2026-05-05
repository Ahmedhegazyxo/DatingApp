import { Gender } from "../enums/gender";

export class ProfileModel {
    public firstName : string = '';
    public lastName : string = '';
    public username : string = '';
    public phoneNumber : string = '';
    public birthdate : string = '';
    public age : number = 0;
    public gender : Gender = Gender.other;
    public email : string = '';
    public bigoraphy : string = '';
}