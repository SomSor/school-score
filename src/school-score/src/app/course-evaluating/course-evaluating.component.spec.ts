import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseEvaluatingComponent } from './course-evaluating.component';

describe('CourseEvaluatingComponent', () => {
  let component: CourseEvaluatingComponent;
  let fixture: ComponentFixture<CourseEvaluatingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CourseEvaluatingComponent]
    });
    fixture = TestBed.createComponent(CourseEvaluatingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
