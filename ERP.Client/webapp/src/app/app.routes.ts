import { Routes } from '@angular/router';
import { companyRoutes } from './features/companies/company.routes';
import { productRoutes } from './features/products/product.routes';

export const routes: Routes = [
    ...companyRoutes,
    ...productRoutes
];
