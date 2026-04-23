import { Routes } from '@angular/router';
import { RegisterationForm } from '../public-components/registeration-form/registeration-form';
import { Home } from '../pages/home/home';
import { Profile } from '../pages/profile/profile';
import { MembersPage } from '../pages/members-page/members-page';
import { authGuard } from '../guards/auth-guard';
import { LoginPage } from '../pages/login-page/login-page';
import { RegisterPage } from '../pages/register-page/register-page';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginPage,
    },
    {
        path: 'register',
        component : RegisterPage
        
    },
    {
        path : '',
        component : Home,
        canActivate : [authGuard]
    },
    {
        path : 'profile',
        component : Profile,
        canActivate : [authGuard]
    },
    {
        path : 'members',
        component : MembersPage,
        canActivate : [authGuard]
    }
];
