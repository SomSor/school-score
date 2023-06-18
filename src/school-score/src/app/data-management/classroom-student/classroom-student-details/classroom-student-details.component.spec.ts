import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomStudentDetailsComponent } from './classroom-student-details.component';

describe('ClassroomStudentDetailsComponent', () => {
  let component: ClassroomStudentDetailsComponent;
  let fixture: ComponentFixture<ClassroomStudentDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClassroomStudentDetailsComponent]
    });
    fixture = TestBed.createComponent(ClassroomStudentDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
