import { MoneyRequestModel } from './money-request.model';

export interface ProductUpdateInformationRequestModel {
    name: string;
    description: string;
    price: MoneyRequestModel
}