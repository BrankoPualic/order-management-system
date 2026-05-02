import { AddressRequestModel } from './address-request.model';

export interface CompanyRegisterRequestModel {
  name: string,
  description: string,
  address: AddressRequestModel
}