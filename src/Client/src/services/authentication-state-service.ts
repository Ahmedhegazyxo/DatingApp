import { Injectable, signal } from "@angular/core";
import { UserModel } from "../models/views/user-model";
import { Router } from "@angular/router";
import { Severity } from "../models/enums/severity";
import { ToastView } from "../models/views/toast-view";
import { ToasterService } from "./general/toaster-service";

@Injectable({
    providedIn: 'root'
})

export class AuthenticationStateService {
    public userModel = signal<UserModel | null>(null);
    protected expiresInMs: number | null = null;
    constructor(protected router: Router, protected toasterService: ToasterService) {
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
        this.setUserModelInfo(userModel);
        this.router.navigate(['/'])
    }
    public logoutTriggered() {
        this.userModel.set(null);
        window.localStorage.removeItem('userModel');
        let toast = new ToastView('Logout', 'Logged out successfully', Severity.Success, 5000);
        this.toasterService.addToast(toast);
        this.router.navigate(['/login'])
    }
    public getUserModelInfo(): UserModel | null {
        let userModelString = window.localStorage.getItem('userModel');
        console.log('local storage accessed');
        if (userModelString == null) {
            return null;
        }
        else {
            let parse = JSON.parse(userModelString as string) as UserModel;
            return parse;
        }
    }
    public setUserModelInfo(userModel: UserModel) {
        this.userModel.set(userModel);
        let toast = new ToastView('Login', 'Logged in successfully', Severity.Success, 3000);
        this.toasterService.addToast(toast);
        window.localStorage.setItem('userModel', JSON.stringify(userModel))
        this.expiresInMs = new Date(userModel.expiresAt).getTime() - new Date().getTime();
        setTimeout(() => {
            this.tokenExpired();
        }, this.expiresInMs);
    }
    private tokenExpired() {
        this.logoutTriggered();
    }
}