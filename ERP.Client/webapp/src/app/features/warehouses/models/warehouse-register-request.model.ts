import { AddressRequestModel } from '../../../shared/models/address/address-request.model';

export interface WarehouseRegisterRequestModel {
    name: string;
    description: string;
    address: AddressRequestModel;
}