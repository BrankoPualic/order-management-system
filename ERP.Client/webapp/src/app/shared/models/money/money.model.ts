export interface Money {
    amount: number;
    currency: string;
}

export type CreateMoneyRequest = Money;
export type UpdateMoneyRequest = Partial<Money>;