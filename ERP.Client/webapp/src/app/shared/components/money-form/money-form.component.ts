import { Component, input } from '@angular/core';
import { FormField } from '@angular/forms/signals';

@Component({
    selector: 'app-money-form',
    imports: [FormField],
    templateUrl: './money-form.component.html',
    styles: ''
})
export class MoneyFormComponent {
    money = input<any>();
}