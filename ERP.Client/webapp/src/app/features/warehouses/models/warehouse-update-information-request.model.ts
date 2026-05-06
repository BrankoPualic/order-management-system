import { UpdateAddressRequest } from '../../../shared/models/address/address.model';

export interface WarehouseUpdateInformationRequestModel {
    name: string;
    description: string;
    address: UpdateAddressRequest;
}