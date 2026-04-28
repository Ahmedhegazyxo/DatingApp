import { Routes } from '@angular/router';
import { Home } from '../components/pages/home/home';
import { Profile } from '../components/pages/profile/profile';
import { MembersPage } from '../components/pages/members-page/members-page';
import { authGuard } from '../guards/auth-guard';
import { LoginPage } from '../components/pages/login-page/login-page';
import { RegisterPage } from '../components/pages/register-page/register-page';
import { MatchesPage } from '../components/pages/matches-page/matches-page';

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
    } ,
    {
        path : 'matches',
        component : MatchesPage,
        canActivate : [authGuard]
    }
];
