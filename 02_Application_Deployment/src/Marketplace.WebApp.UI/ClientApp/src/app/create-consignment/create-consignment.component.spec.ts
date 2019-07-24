import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateConsignmentComponent } from './create-consignment.component';

describe('CreateConsignmentComponent', () => {
  let component: CreateConsignmentComponent;
  let fixture: ComponentFixture<CreateConsignmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateConsignmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateConsignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
