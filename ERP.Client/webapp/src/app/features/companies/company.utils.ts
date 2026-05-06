import { maxLength, required } from '@angular/forms/signals';
import { CompanyForm, RegisterCompanyRequest, UpdateCompanyRequest } from './company.model';
import { addressMapper, emptyAddress } from '../../shared/models/address/address.utils';

const COMPANY_RULES = {
    nameMax: 255
}

export const emptyCompany: CompanyForm = {
    name: '',
    description: '',
    address: emptyAddress
};

export const companySchema = (path: any) => {
    required(path.name);
    required(path.description);
    maxLength(path.name, COMPANY_RULES.nameMax);
};

export const companyMapper = {
    toRegisterRequest(model: CompanyForm): RegisterCompanyRequest {
        return {
            name: model.name.trim(),
            description: model.description.trim(),
            address: addressMapper.toCreateRequest(model.address)
        }
    },
    toUpdateRequest(model: CompanyForm): UpdateCompanyRequest {
        return {
            name: model.name.trim() || undefined,
            description: model.description.trim() || undefined,
            address: addressMapper.toUpdateRequest(model.address) || undefined
        }
    }
};