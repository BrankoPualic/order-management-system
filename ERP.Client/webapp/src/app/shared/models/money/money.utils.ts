import { maxLength, minLength, required } from '@angular/forms/signals';
import { CreateMoneyRequest, Money, MoneyForm, UpdateMoneyRequest } from './money.model';

const MONEY_RULES = {
    currencyMin: 3,
    currencyMax: 3
} as const;

export const emptyMoney: MoneyForm = {
    amount: NaN,
    currency: ''
};

export const moneySchema = (path: any) => {
    required(path.amount);
    required(path.currency);
    minLength(path.currency, MONEY_RULES.currencyMin);
    maxLength(path.currency, MONEY_RULES.currencyMax);
};

export const formatMoney = (money: Money) => `${money.amount} ${money.currency}`;

export const moneyMapper = {
    toCreateRequest(model: MoneyForm): CreateMoneyRequest {
        return {
            amount: model.amount,
            currency: model.currency.trim()
        }
    },
    toUpdateRequest(model: MoneyForm): UpdateMoneyRequest {
        return {
            amount: model.amount || undefined,
            currency: model.currency.trim() || undefined
        }
    }
};