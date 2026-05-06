import { HttpResourceRef } from '@angular/common/http';
import { Component, inject, Signal, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { form, FormField, submit } from '@angular/forms/signals';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { formatAddress } from '../../../shared/models/address/address.utils';
import { Company } from '../company.model';
import { companyMapper, emptyCompany } from '../company.utils';

@Component({
  selector: 'app-company',
  imports: [FormField],
  templateUrl: './company.component.html',
  styles: '',
})
export class CompanyComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  apiService = inject(ApiService);
  companyId: Signal<string | null | undefined>;
  company: HttpResourceRef<Company | undefined>;
  companyModel = signal(emptyCompany);

  companyForm = form(this.companyModel);

  constructor() {
    this.companyId = toSignal(this.route.paramMap.pipe(map(params => params.get('id'))));
    this.company = this.apiService.httpResource<Company | undefined>(`/companies/${this.companyId()}`);
  }

  formatAddress = formatAddress;

  deleteCompany() {
    this.apiService.delete(`/companies/${this.companyId()}`).subscribe({
      next: () => this.router.navigate(['/companies']),
      error: error => console.error(error)
    });
  }

  onSubmit(event: Event) {
    event.preventDefault();
    const request = companyMapper.toUpdateRequest(this.companyModel());
    submit(this.companyForm, async () => {
      this.apiService.patch(`/companies/${this.companyId()}`, request).subscribe({
        next: () => { this.company.reload(); this.companyForm().reset(emptyCompany) },
        error: error => console.error(error)
      });
    })
  }
}