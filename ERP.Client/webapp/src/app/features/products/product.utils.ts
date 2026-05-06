import { maxLength, required } from '@angular/forms/signals';
import { emptyMoney, moneyMapper } from '../../shared/models/money/money.utils';
import { ProductForm, RegisterProductRequest, UpdateProductRequest } from './product.model';

const PRODUCT_RULES = {
    nameMax: 255
} as const;

export const emptyProduct: ProductForm = {
    name: '',
    description: '',
    price: emptyMoney
};

export const productSchema = (path: any) => {
    required(path.name);
    required(path.description);
    maxLength(path.name, PRODUCT_RULES.nameMax);
}

export const productMapper = {
    toRegisterRequest(model: ProductForm): RegisterProductRequest {
        return {
            name: model.name.trim(),
            description: model.description.trim(),
            price: moneyMapper.toCreateRequest(model.price)
        }
    },
    toUpdateRequest(model: ProductForm): UpdateProductRequest {
        return {
            name: model.name.trim() || undefined,
            description: model.description.trim() || undefined,
            price: moneyMapper.toUpdateRequest(model.price) || undefined
        }
    }
};