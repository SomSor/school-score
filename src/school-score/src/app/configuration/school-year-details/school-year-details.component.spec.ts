import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchoolYearDetailsComponent } from './school-year-details.component';

describe('SchoolYearDetailsComponent', () => {
  let component: SchoolYearDetailsComponent;
  let fixture: ComponentFixture<SchoolYearDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SchoolYearDetailsComponent]
    });
    fixture = TestBed.createComponent(SchoolYearDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
