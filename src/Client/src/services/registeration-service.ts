import { HttpClient, HttpErrorResponse, HttpResponse, HttpStatusCode } from "@angular/common/http";
import { RegisterDto } from "../models/dtos/register-dto";
import { Injectable } from "@angular/core";
import { UserModel } from "../models/views/user-model";
import { AuthenticationStateService } from "./authentication-state-service";
import { Router } from "@angular/router";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class RegisterationService {
    private readonly baseUri = 'http://localhost:5138/api/auth'
    private readonly registerUri = '/register'
    constructor(private _httpClient: HttpClient,
        private _authenticationStateService: AuthenticationStateService,
        private router: Router
    ) { }
    public Register(registerDto: RegisterDto): void {
        this._httpClient?.post<UserModel>(this.baseUri + this.registerUri, registerDto, { observe: 'response' })
            .subscribe({
                next: (res: HttpResponse<UserModel>) => this.onAcceptedStatusCallback(res)
            });
    }
    protected onAcceptedStatusCallback(response: HttpResponse<UserModel>): void {
        this._authenticationStateService.loginTriggered(response.body!)
        this.router.navigate(['/']);
    };

}