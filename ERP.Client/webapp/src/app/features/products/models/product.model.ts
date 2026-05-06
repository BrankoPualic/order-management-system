import { Money } from '../../../shared/models/money/money.model';

export interface ProductModel {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    price: Money;
}