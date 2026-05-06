import { AddressRequestModel } from '../../../shared/models/address/address-request.model';

export interface CompanyRegisterRequestModel {
  name: string,
  description: string,
  address: AddressRequestModel;
}