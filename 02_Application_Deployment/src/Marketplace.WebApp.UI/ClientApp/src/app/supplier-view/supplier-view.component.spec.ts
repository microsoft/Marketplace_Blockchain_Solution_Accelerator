import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierViewComponent } from './supplier-view.component';

describe('SupplierViewComponent', () => {
  let component: SupplierViewComponent;
  let fixture: ComponentFixture<SupplierViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
