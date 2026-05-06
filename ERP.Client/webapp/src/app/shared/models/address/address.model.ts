export interface Address {
    street: string;
    city: string;
    state: string;
    country: string;
    zipCode: string;
}

export type AddressForm = Address;
export type CreateAddressRequest = AddressForm;
export type UpdateAddressRequest = Partial<AddressForm>;