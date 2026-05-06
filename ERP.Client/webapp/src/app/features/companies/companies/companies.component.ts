import { DatePipe } from '@angular/common';
import { HttpResourceRef } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterLink } from "@angular/router";
import { ApiService } from '../../../core/services/api.service';
import { formatAddress } from '../../../shared/models/address/address.utils';
import { Company } from '../company.model';

@Component({
  selector: 'app-companies',
  imports: [DatePipe, RouterLink],
  templateUrl: './companies.component.html',
  styles: '',
})
export class CompaniesComponent {
  apiService = inject(ApiService);
  companies: HttpResourceRef<Company[] | undefined>;

  constructor() {
    this.companies = this.apiService.httpResource<Company[] | undefined>('/companies');
  }

  formatAddress = formatAddress;
}