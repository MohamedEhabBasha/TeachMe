import { Routes } from '@angular/router';
import { HomeComponent } from './core/home/home.component';
import { authGuard } from './_guards/auth.guard';
import { RegisterComponent } from './core/authentication/register/register.component';
import { LoginComponent } from './core/authentication/login/login.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
        ]
    },
    {path: '**', component: HomeComponent, pathMatch: 'full'},
];
