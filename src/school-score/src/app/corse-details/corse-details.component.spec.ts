import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CorseDetailsComponent } from './corse-details.component';

describe('CorseDetailsComponent', () => {
  let component: CorseDetailsComponent;
  let fixture: ComponentFixture<CorseDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CorseDetailsComponent]
    });
    fixture = TestBed.createComponent(CorseDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
