import { Address, AddressForm, UpdateAddressRequest } from '../../shared/models/address/address.model';

export interface Warehouse {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    address: Address;
}

export interface WarehouseForm {
    name: string;
    description: string;
    address: AddressForm;
}

export type RegisterWarehouseRequest = WarehouseForm;

export interface UpdateWarehouseRequest {
    name?: string;
    description?: string;
    address: UpdateAddressRequest;
}