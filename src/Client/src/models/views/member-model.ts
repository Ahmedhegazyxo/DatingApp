import { computed } from "@angular/core";
import { Gender } from "../enums/gender";

export class MemberModel {
    public username: string = '';
    public id: string = '';
    public matchId: string | null = null;
    public birthdate: Date = new Date();
    public firstName: string = '';
    public lastName: string = '';
    public gender: Gender = Gender.male;
    public age: number = 0;
    public isLiked: boolean = false;
    public isMatched: boolean = false;
    public profilePhotoId: string | null = null;
}