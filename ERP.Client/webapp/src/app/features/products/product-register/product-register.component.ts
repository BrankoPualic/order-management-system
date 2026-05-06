import { Component, inject, signal } from '@angular/core';
import { form, FormField, maxLength, minLength, required, submit } from '@angular/forms/signals';
import { Router } from '@angular/router';
import { ApiService } from '../../../core/services/api.service';
import { ProductRegisterRequestModel } from '../models/product-register-request.model';
import { applyMoneyValidators } from '../../../shared/models/money/money.utils';

@Component({
  selector: 'app-product-register',
  imports: [FormField],
  templateUrl: './product-register.component.html',
  styleUrl: './product-register.component.css',
})
export class ProductRegisterComponent {
  apiService = inject(ApiService);
  router = inject(Router);

  productModel = signal<ProductRegisterRequestModel>({
    name: '',
    description: '',
    price: {
      amount: NaN,
      currency: 'USD'
    }
  });

  productForm = form(this.productModel, (path) => {
    required(path.name);
    maxLength(path.name, 255);

    required(path.description);

    applyMoneyValidators(path.price);
  });

  onSubmit(event: Event) {
    event.preventDefault();
    submit(this.productForm, async () => {
      this.apiService.post('/products', this.productModel()).subscribe({
        next: id => this.router.navigate(['/products', id]),
        error: error => console.error(error)
      })
    })
  }
}