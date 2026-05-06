import { DatePipe } from '@angular/common';
import { HttpResourceRef } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { formatAddress } from '../../../shared/models/address/address.utils';
import { WarehouseModel } from '../models/warehouse.model';

@Component({
  selector: 'app-warehouses',
  imports: [DatePipe, RouterLink],
  templateUrl: './warehouses.component.html',
  styleUrl: './warehouses.component.css',
})
export class WarehousesComponent {
  apiService = inject(ApiService);
  warehouses: HttpResourceRef<WarehouseModel[] | undefined>;

  constructor() {
    this.warehouses = this.apiService.httpResource<WarehouseModel[] | undefined>('/warehouses');
  }

  formatAddress = formatAddress;
}