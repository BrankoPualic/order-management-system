import { Component, inject } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { HttpResourceRef } from '@angular/common/http';
import { ProductModel } from '../models/product.model';
import { MoneyModel } from '../models/money.model';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

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

  // TODO: Maybe store it as override toString()
  formatPrice = (price: MoneyModel) => `${price.currency} ${price.amount}`;
}
