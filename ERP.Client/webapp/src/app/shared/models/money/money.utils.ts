import { maxLength, minLength, required } from '@angular/forms/signals';
import { Money } from './money.model';

export const MONEY_RULES = {
    currencyMin: 3,
    currencyMax: 3
} as const;

export function applyMoneyValidators(path: any) {
    required(path.amount);

    required(path.currency);
    minLength(path.currency, MONEY_RULES.currencyMin);
    maxLength(path.currency, MONEY_RULES.currencyMax);
}

export function formatMoney(money: Money) {
    return `${money.amount} ${money.currency}`;
}