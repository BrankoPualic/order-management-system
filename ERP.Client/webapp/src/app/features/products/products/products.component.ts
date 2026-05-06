import { DatePipe } from '@angular/common';
import { HttpResourceRef } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { formatMoney } from '../../../shared/models/money/money.utils';
import { Product } from '../product.model';

@Component({
  selector: 'app-products',
  imports: [DatePipe, RouterLink],
  templateUrl: './products.component.html',
  styles: '',
})
export class ProductsComponent {
  apiService = inject(ApiService);
  products: HttpResourceRef<Product[] | undefined>;

  constructor() {
    this.products = this.apiService.httpResource<Product[] | undefined>('/products');
  }

  formatPrice = formatMoney;
}