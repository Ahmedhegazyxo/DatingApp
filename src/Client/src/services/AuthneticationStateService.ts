import { Injectable, signal } from "@angular/core";
import { UserModel } from "../models/views/UserModel";
import { JsonPipe } from "@angular/common";
import { Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})

export class AuthenticationStateService {
    public userModel = signal<UserModel | null>(null);
    public AuhtenticationEventAppTarget: EventTarget = new EventTarget();
    constructor(protected router : Router) {
       this.userModel.set(this.getUserModelInfo()); 
    }  

    public loginTriggered(userModel: UserModel): void {
        const loginEvent: CustomEvent<UserModel> = new CustomEvent<UserModel>('loginevent', {
            detail: userModel
        });
        this.AuhtenticationEventAppTarget.dispatchEvent(loginEvent)
        this.userModel.set(userModel);
        window.localStorage.setItem('userModel', JSON.stringify(userModel))
    }
    public logoutTriggered(){
        const logoutEvent : CustomEvent =  new CustomEvent('logoutevent');
        this.AuhtenticationEventAppTarget.dispatchEvent(logoutEvent);
        this.userModel.set(null);
        this.router.navigate(['/login'])
    }
    public getUserModelInfo(): UserModel | null {
        let userModelString = window.localStorage.getItem('userModel');
        if (userModelString == null) {
            return null;
        }
        else {
            let parse = JSON.parse(userModelString as string) as UserModel;
            return parse;
        }
    }
}