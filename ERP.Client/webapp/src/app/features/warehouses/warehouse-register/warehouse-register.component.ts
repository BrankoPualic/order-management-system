import { Component, inject, signal } from '@angular/core';
import { form, FormField, submit } from '@angular/forms/signals';
import { Router } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { addressSchema } from '../../../shared/models/address/address.utils';
import { emptyWarehouse, warehouseMapper, warehouseSchema } from '../warehouse.utils';
import { AddressFormComponent } from "../../../shared/components/address-form/address-form.component";

@Component({
  selector: 'app-warehouse-register',
  imports: [FormField, AddressFormComponent],
  templateUrl: './warehouse-register.component.html',
  styles: '',
})
export class WarehouseRegisterComponent {
  apiService = inject(ApiService);
  router = inject(Router);
  warehouseModel = signal(emptyWarehouse);
  warehouseForm = form(this.warehouseModel, (path) => {
    warehouseSchema(path);
    addressSchema(path.address);
  });

  onSubmit(event: Event) {
    event.preventDefault();
    const request = warehouseMapper.toRegisterRequest(this.warehouseModel());
    submit(this.warehouseForm, async () => {
      this.apiService.post('/warehouses', request).subscribe({
        next: id => this.router.navigate(['/warehouses', id]),
        error: error => console.error(error)
      });
    })
  }
}