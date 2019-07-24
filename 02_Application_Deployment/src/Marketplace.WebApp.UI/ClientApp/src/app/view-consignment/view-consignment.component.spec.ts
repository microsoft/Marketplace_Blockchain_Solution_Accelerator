import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewConsignmentComponent } from './view-consignment.component';

describe('ViewConsignmentComponent', () => {
  let component: ViewConsignmentComponent;
  let fixture: ComponentFixture<ViewConsignmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewConsignmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewConsignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
