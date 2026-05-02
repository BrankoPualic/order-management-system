import { Routes } from '@angular/router';

export const companyRoutes: Routes = [
    {
        path: 'companies/register',
        title: 'Register Company',
        loadComponent: () => import('./company-register.component/company-register.component').then(_ => _.CompanyRegisterComponent)
    }
]