import { HttpResourceRef } from '@angular/common/http';
import { Component, inject, signal, Signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { form, FormField, submit } from '@angular/forms/signals';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { formatAddress } from '../../../shared/models/address/address.utils';
import { Warehouse } from '../warehouse.model';
import { emptyWarehouse, warehouseMapper } from '../warehouse.utils';
import { AddressFormComponent } from "../../../shared/components/address-form/address-form.component";

@Component({
  selector: 'app-warehouse',
  imports: [FormField, AddressFormComponent],
  templateUrl: './warehouse.component.html',
  styles: '',
})
export class WarehouseComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  apiService = inject(ApiService);
  warehouseId: Signal<string | null | undefined>;
  warehouse: HttpResourceRef<Warehouse | undefined>;
  warehouseModel = signal(emptyWarehouse);
  warehouseForm = form(this.warehouseModel);

  constructor() {
    this.warehouseId = toSignal(this.route.paramMap.pipe(map(params => params.get('id'))));
    this.warehouse = this.apiService.httpResource<Warehouse | undefined>(`/companies/${this.warehouseId()}`);
  }

  formatAddress = formatAddress;

  deleteWarehouse() {
    this.apiService.delete(`/warehouses/${this.warehouseId()}`).subscribe({
      next: () => this.router.navigate(['/warehouses']),
      error: error => console.error(error)
    })
  }

  onSubmit(event: Event) {
    event.preventDefault();
    const request = warehouseMapper.toUpdateRequest(this.warehouseModel());
    submit(this.warehouseForm, async () => {
      this.apiService.patch(`/companies/${this.warehouseId()}`, request).subscribe({
        next: () => this.warehouse.reload(),
        error: error => console.error(error)
      });
    })
  }
}