import { maxLength, required } from '@angular/forms/signals';
import { Address, AddressForm, CreateAddressRequest, UpdateAddressRequest } from './address.model';

const ADDRESS_RULES = {
    streetMax: 255,
    cityMax: 100,
    stateMax: 100,
    countryMax: 100,
    zipCodeMax: 20
} as const;

export const emptyAddress: AddressForm = {
    street: '',
    city: '',
    state: '',
    country: '',
    zipCode: ''
};

export const addressSchema = (path: any) => {
    required(path.street);
    required(path.city);
    required(path.state);
    required(path.country);
    required(path.zipCode);
    maxLength(path.street, ADDRESS_RULES.streetMax);
    maxLength(path.city, ADDRESS_RULES.cityMax);
    maxLength(path.state, ADDRESS_RULES.stateMax);
    maxLength(path.country, ADDRESS_RULES.countryMax);
    maxLength(path.zipCode, ADDRESS_RULES.zipCodeMax);
};

export const formatAddress = (address: Address) => `${address.street}, ${address.zipCode} ${address.city}, ${address.state} ${address.country}`;

export const addressMapper = {
    toCreateRequest(model: AddressForm): CreateAddressRequest {
        return {
            street: model.street.trim(),
            city: model.city.trim(),
            state: model.state.trim(),
            country: model.country.trim(),
            zipCode: model.zipCode.trim()
        }
    },
    toUpdateRequest(model: AddressForm): UpdateAddressRequest {
        return {
            street: model.street.trim() || undefined,
            city: model.city.trim() || undefined,
            state: model.state.trim() || undefined,
            country: model.country.trim() || undefined,
            zipCode: model.zipCode.trim() || undefined
        }
    }
};