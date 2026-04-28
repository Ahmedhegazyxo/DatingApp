import { HttpClient, HttpErrorResponse, HttpResponse, HttpStatusCode } from "@angular/common/http";
import { loginDto } from "../models/dtos/loginDto";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationStateService } from "./authentication-state-service";
import { UserModel } from "../models/views/UserModel";
import { ToasterService } from "./toaster-service";
import { ToastView } from "../models/views/ToastView";
import { Severity } from "../models/enums/severity";

@Injectable({
    providedIn: 'root'
})
export class LoginService {
    private readonly baseUri = 'http://localhost:5138/api/auth' 
    private readonly loginUri = '/login' 
    constructor(private httpClient: HttpClient,
        private router: Router,
        private authenticationStateService: AuthenticationStateService,
    private toasterService: ToasterService) { }
        public Logout() {
            window.localStorage.removeItem('userModel');
            this.authenticationStateService.logoutTriggered();
        }
    
        public Login(username: string
        , password: string
    ): void {

        let _loginDto: loginDto = new loginDto();
        _loginDto.Username = username;
        _loginDto.Password = password;
        this.httpClient?.post<UserModel>(this.baseUri + this.loginUri , _loginDto, { observe: 'response' })
            .subscribe({
                next: (res: HttpResponse<UserModel>) => this.onAcceptedStatusCallback(res)
            });

    }
    protected onAcceptedStatusCallback(response: HttpResponse<UserModel>): void {
        this.authenticationStateService.AuhtenticationEventAppTarget.addEventListener('loginevent', (e: Event) => {
            const event = e as CustomEvent<UserModel>;
            
        });
        this.authenticationStateService.loginTriggered(response.body as UserModel);
        this.router.navigate(['/']);
        let toast = new ToastView();
        
    }
}