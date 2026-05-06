import { UpdateAddressRequest } from '../../../shared/models/address/address.model';

export interface WarehouseRegisterRequestModel {
    name: string;
    description: string;
    address: UpdateAddressRequest;
}