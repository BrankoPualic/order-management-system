import { MoneyModel } from '../../../shared/models/money.model';

export interface ProductModel {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    price: MoneyModel;
}