import { DatePipe } from '@angular/common';
import { HttpResourceRef } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { formatAddress } from '../../../shared/models/address/address.utils';
import { Warehouse } from '../warehouse.model';

@Component({
  selector: 'app-warehouses',
  imports: [DatePipe, RouterLink],
  templateUrl: './warehouses.component.html',
  styles: '',
})
export class WarehousesComponent {
  apiService = inject(ApiService);
  warehouses: HttpResourceRef<Warehouse[] | undefined>;

  constructor() {
    this.warehouses = this.apiService.httpResource<Warehouse[] | undefined>('/warehouses');
  }

  formatAddress = formatAddress;
}