import { Component, input } from '@angular/core';
import { FormField } from '@angular/forms/signals';

@Component({
    selector: 'app-address-form',
    imports: [FormField],
    templateUrl: './address-form.component.html',
    styles: ''
})
export class AddressFormComponent {
    address = input<any>();
}