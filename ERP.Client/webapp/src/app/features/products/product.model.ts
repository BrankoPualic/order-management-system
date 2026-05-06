import { Money, MoneyForm, UpdateMoneyRequest } from '../../shared/models/money/money.model';

export interface Product {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    price: Money;
}

export interface ProductForm {
    name: string;
    description: string;
    price: MoneyForm;
}

export type RegisterProductRequest = ProductForm;

export interface UpdateProductRequest {
    name?: string;
    description?: string;
    price?: UpdateMoneyRequest;
}