import { Component, inject, signal } from '@angular/core';
import { form, maxLength, required, submit, FormField } from '@angular/forms/signals';
import { ApiService } from '../../../core/services/api.service';
import { Router } from '@angular/router';
import { CompanyRegisterRequestModel } from '../models/company-register-request.model';
import { applyAddressValidators } from '../../../shared/models/address/address.utils';

@Component({
  selector: 'app-company-register',
  imports: [FormField],
  templateUrl: './company-register.component.html',
  styleUrl: './company-register.component.css',
})
export class CompanyRegisterComponent {
  apiService = inject(ApiService);
  router = inject(Router);

  companyModel = signal<CompanyRegisterRequestModel>({
    name: '',
    description: '',
    address: {
      street: '',
      city: '',
      state: '',
      country: '',
      zipCode: ''
    }
  });

  companyForm = form(this.companyModel, (path) => {
    required(path.name);
    // TODO: Probably should pull this rule into one company domain class where we can see domain rules. Same thing for api aggregate
    maxLength(path.name, 255);

    required(path.description);

    applyAddressValidators(path.address);
  });

  onSubmit(event: Event) {
    event.preventDefault();
    submit(this.companyForm, async () => {
      this.apiService.post('/companies', this.companyModel()).subscribe({
        next: id => this.router.navigate(['/companies', id]),
        error: error => console.error(error)
      });
    })
  }
}