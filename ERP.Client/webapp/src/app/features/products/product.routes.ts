import { Routes } from '@angular/router';

export const productRoutes: Routes = [
    {
        path: 'products/register',
        title: 'Register Product',
        loadComponent: () => import('./product-register/product-register.component').then(_ => _.ProductRegisterComponent)
    }
]