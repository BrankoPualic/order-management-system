import { deepCopy } from '@angular-devkit/core';
import { HttpResourceRef } from '@angular/common/http';
import { Component, effect, inject, signal, Signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { MoneyModel } from '../models/money.model';
import { ProductUpdateInformationRequestModel } from '../models/product-update-information-request.model';
import { ProductModel } from '../models/product.model';

@Component({
  selector: 'app-product.component',
  imports: [FormsModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
})
export class ProductComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  apiService = inject(ApiService);
  productId: Signal<string | null | undefined>;
  product: HttpResourceRef<ProductModel | undefined>;
  productCopy = signal<ProductUpdateInformationRequestModel>({} as ProductUpdateInformationRequestModel);

  constructor() {
    this.productId = toSignal(this.route.paramMap.pipe(map(params => params.get('id'))));
    this.product = this.apiService.httpResource<ProductModel | undefined>(`/products/${this.productId()}`);

    effect(() => {
      if (this.product.value()) this.productCopy.set(deepCopy(this.product.value()) as ProductUpdateInformationRequestModel);
    })
  }

  formatPrice = (price: MoneyModel) => `${price.amount} ${price.currency}`;

  deleteProduct() {
    this.apiService.delete(`/products/${this.productId()}`).subscribe({
      next: () => this.router.navigate(['/products']),
      error: error => console.error(error)
    })
  }

  updateProduct() {
    this.apiService.patch(`/products/${this.productId()}`, this.productCopy()).subscribe({
      next: () => this.product.reload(),
      error: error => console.error(error)
    })
  }
}
