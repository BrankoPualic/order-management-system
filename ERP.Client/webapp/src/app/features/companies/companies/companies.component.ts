import { Component, inject } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { HttpResourceRef } from '@angular/common/http';
import { CompanyModel } from '../models/company.model';
import { DatePipe } from '@angular/common';
import { AddressModel } from '../models/address.model';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-companies.component',
  imports: [DatePipe, RouterLink],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.css',
})
export class CompaniesComponent {
  apiService = inject(ApiService);
  companies: HttpResourceRef<CompanyModel[] | undefined>;

  constructor() {
    this.companies = this.apiService.httpResource<CompanyModel[] | undefined>('/companies');
  }

  formatAddress = (address: AddressModel) => `${address.street}, ${address.zipCode} ${address.city}, ${address.state} ${address.country}`;
}