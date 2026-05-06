import { Address, AddressForm, UpdateAddressRequest } from '../../shared/models/address/address.model';

export interface Company {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    address: Address;
}

export interface CompanyForm {
    name: string;
    description: string;
    address: AddressForm;
}

export type RegisterCompanyRequest = CompanyForm;

export interface UpdateCompanyRequest {
    name?: string;
    description?: string;
    address?: UpdateAddressRequest;
}