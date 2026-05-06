import { AddressRequestModel } from '../../../shared/models/address-request.model';

export interface WarehouseUpdateInformationRequestModel {
    name: string;
    description: string;
    address: AddressRequestModel;
}