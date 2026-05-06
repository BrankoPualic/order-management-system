import { deepCopy } from '@angular-devkit/core';
import { HttpResourceRef } from '@angular/common/http';
import { Component, effect, inject, Signal, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { AddressModel } from '../../../shared/models/address.model';
import { CompanyUpdateInformationRequestModel } from '../models/company-update-information-request.model';
import { CompanyModel } from '../models/company.model';

@Component({
  selector: 'app-company',
  imports: [FormsModule],
  templateUrl: './company.component.html',
  styleUrl: './company.component.css',
})
export class CompanyComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  apiService = inject(ApiService);
  companyId: Signal<string | null | undefined>;
  company: HttpResourceRef<CompanyModel | undefined>;
  companyCopy = signal<CompanyUpdateInformationRequestModel>({} as CompanyUpdateInformationRequestModel);

  constructor() {
    this.companyId = toSignal(this.route.paramMap.pipe(map(params => params.get('id'))));
    this.company = this.apiService.httpResource<CompanyModel | undefined>(`/companies/${this.companyId()}`);

    effect(() => {
      if (this.company.value()) this.companyCopy.set(deepCopy(this.company.value()) as CompanyUpdateInformationRequestModel);
    })
  }

  formatAddress = (address: AddressModel) => `${address.street}<br/>${address.zipCode} ${address.city}, ${address.state}<br/>${address.country}`;

  deleteCompany() {
    this.apiService.delete(`/companies/${this.companyId()}`).subscribe({
      next: () => this.router.navigate(['/companies']),
      error: error => console.error(error)
    })
  }

  updateCompany() {
    this.apiService.patch(`/companies/${this.companyId()}`, this.companyCopy()).subscribe({
      next: () => this.company.reload(),
      error: error => console.error(error)
    })
  }
}