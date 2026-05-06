import { AddressRequestModel } from '../../../shared/models/address/address-request.model';

export interface CompanyUpdateInformationRequestModel {
    name: string;
    description: string;
    address: AddressRequestModel
}