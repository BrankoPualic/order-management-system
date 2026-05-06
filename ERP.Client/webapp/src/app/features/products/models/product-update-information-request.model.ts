import { MoneyRequestModel } from '../../../shared/models/money-request.model';

export interface ProductUpdateInformationRequestModel {
    name: string;
    description: string;
    price: MoneyRequestModel
}