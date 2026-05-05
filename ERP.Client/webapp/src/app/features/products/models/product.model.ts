import { MoneyModel } from './money.model';

export interface ProductModel {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    price: MoneyModel;
}