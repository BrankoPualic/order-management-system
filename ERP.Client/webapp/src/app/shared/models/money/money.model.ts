export interface Money {
    amount: number;
    currency: string;
}

export type MoneyForm = Money;
export type CreateMoneyRequest = MoneyForm;
export type UpdateMoneyRequest = Partial<MoneyForm>;