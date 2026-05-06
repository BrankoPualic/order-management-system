import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WarehouseRegisterComponent } from './warehouse-register.component';

describe('WarehouseRegisterComponent', () => {
  let component: WarehouseRegisterComponent;
  let fixture: ComponentFixture<WarehouseRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WarehouseRegisterComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(WarehouseRegisterComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
