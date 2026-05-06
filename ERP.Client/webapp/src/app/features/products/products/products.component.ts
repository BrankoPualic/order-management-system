import { DatePipe } from '@angular/common';
import { HttpResourceRef } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { formatMoney } from '../../../shared/models/money/money.utils';
import { ProductModel } from '../models/product.model';

@Component({
  selector: 'app-products',
  imports: [DatePipe, RouterLink],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent {
  apiService = inject(ApiService);
  products: HttpResourceRef<ProductModel[] | undefined>;

  constructor() {
    this.products = this.apiService.httpResource<ProductModel[] | undefined>('/products');
  }

  formatPrice = formatMoney;
}