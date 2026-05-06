import { Address } from '../../../shared/models/address/address.model';

export interface WarehouseModel {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    address: Address;
}