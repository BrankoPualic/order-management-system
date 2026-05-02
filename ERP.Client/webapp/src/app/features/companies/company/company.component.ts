import { HttpResourceRef } from '@angular/common/http';
import { Component, inject, model, Signal, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { FormsModule } from '@angular/forms';
import { CompanyModel } from '../models/company.model';
import { AddressModel } from '../models/address.model';

@Component({
  selector: 'app-company.component',
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
  newName = '';
  newDescription = '';
  newAddress = signal<AddressModel>({
    street: '',
    city: '',
    state: '',
    country: '',
    zipCode: ''
  });

  constructor() {
    this.companyId = toSignal(this.route.paramMap.pipe(map(params => params.get('id'))));
    this.company = this.apiService.httpResource<CompanyModel | undefined>(`/companies/${this.companyId()}`);
  }

  formatAddress = (address: AddressModel) => `${address.street}<br/>${address.zipCode} ${address.city}, ${address.state}<br/>${address.country}`;

  deleteCompany() {
    this.apiService.delete(`/companies/${this.companyId()}`).subscribe({
      next: () => this.router.navigate(['/companies/register']),
      error: error => console.error(error)
    })
  }

  rename() {
    this.apiService.patch(`/companies/${this.companyId()}/name`, { name: this.newName }).subscribe({
      next: () => { this.newName = ''; this.company.reload() },
      error: error => console.error(error)
    })
  }

  changeDescription() {
    this.apiService.patch(`/companies/${this.companyId()}/description`, { description: this.newDescription }).subscribe({
      next: () => { this.newDescription = ''; this.company.reload() },
      error: error => console.error(error)
    })
  }

  changeAddress() {
    this.apiService.patch(`/companies/${this.companyId()}/address`, { address: this.newAddress() }).subscribe({
      next: () => { this.newAddress.set({} as AddressModel); this.company.reload() },
      error: error => console.error(error)
    })
  }
}