import { Component, inject, signal } from '@angular/core';
import { form, FormField, submit } from '@angular/forms/signals';
import { Router } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { moneySchema } from '../../../shared/models/money/money.utils';
import { emptyProduct, productMapper, productSchema } from '../product.utils';
import { MoneyFormComponent } from "../../../shared/components/money-form/money-form.component";

@Component({
  selector: 'app-product-register',
  imports: [FormField, MoneyFormComponent],
  templateUrl: './product-register.component.html',
  styles: '',
})
export class ProductRegisterComponent {
  apiService = inject(ApiService);
  router = inject(Router);
  productModel = signal(emptyProduct);
  productForm = form(this.productModel, (path) => {
    productSchema(path);
    moneySchema(path.price);
  });

  onSubmit(event: Event) {
    event.preventDefault();
    const request = productMapper.toRegisterRequest(this.productModel());
    submit(this.productForm, async () => {
      this.apiService.post('/products', request).subscribe({
        next: id => this.router.navigate(['/products', id]),
        error: error => console.error(error)
      })
    })
  }
}