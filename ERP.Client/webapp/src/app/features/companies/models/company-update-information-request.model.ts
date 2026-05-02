import { AddressRequestModel } from './address-request.model';

export interface CompanyUpdateInformationRequestModel {
    name: string;
    description: string;
    address: AddressRequestModel
}