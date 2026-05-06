import { HttpResourceRef } from '@angular/common/http';
import { Component, inject, signal, Signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { form, FormField, submit } from '@angular/forms/signals';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { formatMoney } from '../../../shared/models/money/money.utils';
import { Product } from '../product.model';
import { emptyProduct, productMapper } from '../product.utils';
import { MoneyFormComponent } from "../../../shared/components/money-form/money-form.component";

@Component({
  selector: 'app-product.component',
  imports: [FormField, MoneyFormComponent],
  templateUrl: './product.component.html',
  styles: '',
})
export class ProductComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  apiService = inject(ApiService);
  productId: Signal<string | null | undefined>;
  product: HttpResourceRef<Product | undefined>;
  productModel = signal(emptyProduct);
  productForm = form(this.productModel);

  constructor() {
    this.productId = toSignal(this.route.paramMap.pipe(map(params => params.get('id'))));
    this.product = this.apiService.httpResource<Product | undefined>(`/products/${this.productId()}`);
  }

  formatPrice = formatMoney;

  deleteProduct() {
    this.apiService.delete(`/products/${this.productId()}`).subscribe({
      next: () => this.router.navigate(['/products']),
      error: error => console.error(error)
    })
  }

  onSubmit(event: Event) {
    event.preventDefault();
    const request = productMapper.toUpdateRequest(this.productModel());
    submit(this.productForm, async () => {
      this.apiService.patch(`/products/${this.productId()}`, request).subscribe({
        next: () => this.product.reload(),
        error: error => console.error(error)
      });
    })
  }
}