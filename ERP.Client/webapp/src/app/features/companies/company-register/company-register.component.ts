import { Component, inject, signal } from '@angular/core';
import { form, maxLength, required, submit, FormField } from '@angular/forms/signals';
import { ApiService } from '../../../core/services/api.service';
import { Router } from '@angular/router';
import { CompanyRegisterRequestModel } from '../models/company-register-request.model';

@Component({
  selector: 'app-company-register.component',
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

    required(path.address.street);
    maxLength(path.address.street, 255);

    required(path.address.city);
    maxLength(path.address.city, 100);

    required(path.address.state);
    maxLength(path.address.state, 100);

    required(path.address.country);
    maxLength(path.address.country, 100);

    required(path.address.zipCode);
    maxLength(path.address.city, 20);
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