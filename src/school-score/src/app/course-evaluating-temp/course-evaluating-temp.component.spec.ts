import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseEvaluatingTempComponent } from './course-evaluating-temp.component';

describe('CourseEvaluatingTempComponent', () => {
  let component: CourseEvaluatingTempComponent;
  let fixture: ComponentFixture<CourseEvaluatingTempComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CourseEvaluatingTempComponent]
    });
    fixture = TestBed.createComponent(CourseEvaluatingTempComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
