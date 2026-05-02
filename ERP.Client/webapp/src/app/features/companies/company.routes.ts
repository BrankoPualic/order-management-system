import { Routes } from '@angular/router';

export const companyRoutes: Routes = [
    {
        path: 'companies/register',
        title: 'Register Company',
        loadComponent: () => import('./company-register.component/company-register.component').then(_ => _.CompanyRegisterComponent)
    },
    {
        path: 'companies/:id',
        title: 'Company',
        loadComponent: () => import('./company.component/company.component').then(_ => _.CompanyComponent)
    }
]