import { Routes } from '@angular/router';

export const companyRoutes: Routes = [
    {
        path: 'companies/register',
        title: 'Register Company',
        loadComponent: () => import('./company-register/company-register.component').then(_ => _.CompanyRegisterComponent)
    },
    {
        path: 'companies/:id',
        title: 'Company',
        loadComponent: () => import('./company/company.component').then(_ => _.CompanyComponent)
    },
    {
        path: 'companies',
        title: 'Companies',
        loadComponent: () => import('./companies/companies.component').then(_ => _.CompaniesComponent)
    }
]