import { Component, inject, signal } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { Router } from '@angular/router';
import { WarehouseRegisterRequestModel } from '../models/warehouse-register-request.model';
import { form, FormField, maxLength, required, submit } from '@angular/forms/signals';

@Component({
  selector: 'app-warehouse-register',
  imports: [FormField],
  templateUrl: './warehouse-register.component.html',
  styleUrl: './warehouse-register.component.css',
})
export class WarehouseRegisterComponent {
  apiService = inject(ApiService);
  router = inject(Router);

  warehouseModel = signal<WarehouseRegisterRequestModel>({
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

  warehouseForm = form(this.warehouseModel, (path) => {
    required(path.name);
    // TODO: Probably should pull this rule into one warehouse domain class where we can see domain rules. Same thing for api aggregate
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
    submit(this.warehouseForm, async () => {
      this.apiService.post('/warehouses', this.warehouseModel()).subscribe({
        next: id => this.router.navigate(['/warehouses', id]),
        error: error => console.error(error)
      });
    })
  }
}
