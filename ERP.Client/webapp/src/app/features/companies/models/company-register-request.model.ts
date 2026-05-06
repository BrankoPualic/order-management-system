import { AddressRequestModel } from '../../../shared/models/address-request.model';

export interface CompanyRegisterRequestModel {
  name: string,
  description: string,
  address: AddressRequestModel;
}