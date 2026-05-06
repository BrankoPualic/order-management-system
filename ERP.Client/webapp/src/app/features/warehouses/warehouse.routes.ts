import { Routes } from '@angular/router';

export const warehouseRoutes: Routes = [
    {
        path: 'warehouses/register',
        title: 'Regsiter Warehouse',
        loadComponent: () => import('./warehouse-register/warehouse-register.component').then(_ => _.WarehouseRegisterComponent)
    },
    {
        path: 'warehouses/:id',
        title: 'Warehouse',
        loadComponent: () => import('./warehouse/warehouse.component').then(_ => _.WarehouseComponent)
    },
    {
        path: 'warehouses',
        title: 'Warehouses',
        loadComponent: () => import('./warehouses/warehouses.component').then(_ => _.WarehousesComponent)
    }
]