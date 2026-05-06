export interface Address {
    street: string;
    city: string;
    state: string;
    country: string;
    zipCode: string;
}

export type CreateAddressRequest = Address;
export type UpdateAddressRequest = Partial<Address>;