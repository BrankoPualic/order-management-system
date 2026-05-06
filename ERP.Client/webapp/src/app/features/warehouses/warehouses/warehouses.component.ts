import { DatePipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { WarehouseModel } from '../models/warehouse.model';
import { HttpResourceRef } from '@angular/common/http';
import { AddressModel } from '../../../shared/models/address.model';

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

  // TODO: Maybe store it as override toString()?
  formatAddress = (address: AddressModel) => `${address.street}, ${address.zipCode} ${address.city}, ${address.state} ${address.country}`;
}