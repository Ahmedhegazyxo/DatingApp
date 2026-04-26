import { Injectable, signal } from "@angular/core";
import { UserModel } from "../models/views/UserModel";
import { JsonPipe } from "@angular/common";
import { Router } from "@angular/router";
import { timer } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class AuthenticationStateService {
    public userModel = signal<UserModel | null>(null);
    public AuhtenticationEventAppTarget: EventTarget = new EventTarget();
    protected expiresInMs: number | null = null;
    constructor(protected router: Router) {
        this.userModel.set(this.getUserModelInfo());
        if (this.userModel() == null) {
            this.expiresInMs = 0;
        }
        else {
            this.expiresInMs = new Date(this.userModel()!.expiresAt).getTime() - new Date().getTime();
            if (this.expiresInMs <= 0) {
                this.tokenExpired();
            }
            else {
                setTimeout(() => { this.tokenExpired() }, this.expiresInMs);
            }
        }
    }

    public loginTriggered(userModel: UserModel): void {
        const loginEvent: CustomEvent<UserModel> = new CustomEvent<UserModel>('loginevent', {
            detail: userModel
        });
        this.AuhtenticationEventAppTarget.dispatchEvent(loginEvent)
        this.userModel.set(userModel);
        window.localStorage.setItem('userModel', JSON.stringify(userModel))
        this.expiresInMs = new Date(userModel.expiresAt).getTime() - new Date().getTime();
        setTimeout(() => {
            this.tokenExpired();
        }, this.expiresInMs);
    }
    public logoutTriggered() {
        const logoutEvent: CustomEvent = new CustomEvent('logoutevent');
        this.AuhtenticationEventAppTarget.dispatchEvent(logoutEvent);
        this.userModel.set(null);
        window.localStorage.removeItem('userModel');
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
    private tokenExpired() {
        this.logoutTriggered();
    }
}