import { computed } from "@angular/core";
import { Gender } from "../enums/gender";

export class MemberModel {
    public username: string = '';
    public id: string = '';
    public birthdate: Date = new Date();
    public firstName: string = '';
    public lastName: string = '';
    public gender: Gender = Gender.male;
    public age : number = 0;
    public isLikedBefore : boolean = false;
    public profilePhotoId : string | null = null;
}