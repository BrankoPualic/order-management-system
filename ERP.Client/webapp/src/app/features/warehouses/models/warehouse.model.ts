import { AddressModel } from '../../../shared/models/address.model';

export interface WarehouseModel {
    id: string;
    name: string;
    description: string;
    createdOn: Date | string;
    address: AddressModel;
}