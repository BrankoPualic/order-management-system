import { maxLength, required } from '@angular/forms/signals';
import { Address } from './address.model';

export const ADDRESS_RULES = {
    streetMax: 255,
    cityMax: 100,
    stateMax: 100,
    countryMax: 100,
    zipCodeMax: 20
} as const;

export function applyAddressValidators(path: any) {
    required(path.street);
    maxLength(path.street, ADDRESS_RULES.streetMax);

    required(path.city);
    maxLength(path.city, ADDRESS_RULES.cityMax);

    required(path.state);
    maxLength(path.state, ADDRESS_RULES.stateMax);

    required(path.country);
    maxLength(path.country, ADDRESS_RULES.countryMax);

    required(path.zipCode);
    maxLength(path.zipCode, ADDRESS_RULES.zipCodeMax);
}

export function formatAddress(address: Address) {
    return `${address.street}, ${address.zipCode} ${address.city}, ${address.state} ${address.country}`;
}