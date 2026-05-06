import { Component, inject, signal } from '@angular/core';
import { form, FormField, submit } from '@angular/forms/signals';
import { Router } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { companyMapper, companySchema, emptyCompany } from '../company.utils';
import { addressSchema } from '../../../shared/models/address/address.utils';
import { AddressFormComponent } from "../../../shared/components/address-form/address-form.component";

@Component({
  selector: 'app-company-register',
  imports: [FormField, AddressFormComponent],
  templateUrl: './company-register.component.html',
  styles: '',
})
export class CompanyRegisterComponent {
  apiService = inject(ApiService);
  router = inject(Router);
  companyModel = signal(emptyCompany);
  companyForm = form(this.companyModel, (path) => {
    companySchema(path);
    addressSchema(path.address);
  });

  onSubmit(event: Event) {
    event.preventDefault();
    const request = companyMapper.toRegisterRequest(this.companyModel());
    submit(this.companyForm, async () => {
      this.apiService.post('/companies', request).subscribe({
        next: id => this.router.navigate(['/companies', id]),
        error: error => console.error(error)
      });
    })
  }
}