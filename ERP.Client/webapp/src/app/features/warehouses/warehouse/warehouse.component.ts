import { Component, effect, inject, signal, Signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { HttpResourceRef } from '@angular/common/http';
import { WarehouseModel } from '../models/warehouse.model';
import { WarehouseUpdateInformationRequestModel } from '../models/warehouse-update-information-request.model';
import { toSignal } from '@angular/core/rxjs-interop';
import { map } from 'rxjs';
import { deepCopy } from '@angular-devkit/core';
import { AddressModel } from '../../../shared/models/address.model';

@Component({
  selector: 'app-warehouse',
  imports: [FormsModule],
  templateUrl: './warehouse.component.html',
  styleUrl: './warehouse.component.css',
})
export class WarehouseComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  apiService = inject(ApiService);
  warehouseId: Signal<string | null | undefined>;
  warehouse: HttpResourceRef<WarehouseModel | undefined>;
  warehouseCopy = signal<WarehouseUpdateInformationRequestModel>({} as WarehouseUpdateInformationRequestModel);

  constructor() {
    this.warehouseId = toSignal(this.route.paramMap.pipe(map(params => params.get('id'))));
    this.warehouse = this.apiService.httpResource<WarehouseModel | undefined>(`/companies/${this.warehouseId()}`);

    effect(() => {
      if (this.warehouse.value()) this.warehouseCopy.set(deepCopy(this.warehouse.value()) as WarehouseUpdateInformationRequestModel);
    })
  }

  formatAddress = (address: AddressModel) => `${address.street}<br/>${address.zipCode} ${address.city}, ${address.state}<br/>${address.country}`;

  deleteWarehouse() {
    this.apiService.delete(`/warehouses/${this.warehouseId()}`).subscribe({
      next: () => this.router.navigate(['/warehouses']),
      error: error => console.error(error)
    })
  }

  updateWarehouse() {
    this.apiService.patch(`/companies/${this.warehouseId()}`, this.warehouseCopy()).subscribe({
      next: () => this.warehouse.reload(),
      error: error => console.error(error)
    })
  }
}