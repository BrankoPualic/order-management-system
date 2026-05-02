import { AddressModel } from './address.model';

export interface CompanyModel {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    address: AddressModel;
}