import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseCheckComponent } from './course-check.component';

describe('CourseCheckComponent', () => {
  let component: CourseCheckComponent;
  let fixture: ComponentFixture<CourseCheckComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CourseCheckComponent]
    });
    fixture = TestBed.createComponent(CourseCheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
