import { required, maxLength } from '@angular/forms/signals';
import { addressMapper, emptyAddress } from '../../shared/models/address/address.utils';
import { RegisterWarehouseRequest, UpdateWarehouseRequest, WarehouseForm } from './warehouse.model';

const WAREHOUSE_RULES = {
    nameMax: 255
} as const;

export const emptyWarehouse: WarehouseForm = {
    name: '',
    description: '',
    address: emptyAddress
};

export const warehouseSchema = (path: any) => {
    required(path.name);
    required(path.description);
    maxLength(path.name, WAREHOUSE_RULES.nameMax);
};

export const warehouseMapper = {
    toRegisterRequest(model: WarehouseForm): RegisterWarehouseRequest {
        return {
            name: model.name.trim(),
            description: model.description.trim(),
            address: addressMapper.toCreateRequest(model.address)
        }
    },
    toUpdateRequest(model: WarehouseForm): UpdateWarehouseRequest {
        return {
            name: model.name.trim() || undefined,
            description: model.description.trim() || undefined,
            address: addressMapper.toUpdateRequest(model.address) || undefined
        }
    }
};