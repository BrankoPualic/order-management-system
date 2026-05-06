import { Address } from '../../../shared/models/address/address.model';

export interface CompanyModel {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    address: Address;
}