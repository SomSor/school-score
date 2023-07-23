import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagingControlsComponent } from './paging-controls.component';

describe('PagingControlsComponent', () => {
  let component: PagingControlsComponent;
  let fixture: ComponentFixture<PagingControlsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PagingControlsComponent]
    });
    fixture = TestBed.createComponent(PagingControlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
