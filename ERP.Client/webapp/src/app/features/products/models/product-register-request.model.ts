import { MoneyRequestModel } from './money-request.model';

export interface ProductRegisterRequestModel {
    name: string;
    description: string;
    price: MoneyRequestModel
}