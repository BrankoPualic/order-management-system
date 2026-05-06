import { MoneyRequestModel } from '../../../shared/models/money-request.model';

export interface ProductRegisterRequestModel {
    name: string;
    description: string;
    price: MoneyRequestModel
}