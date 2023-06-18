import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomStudentCreateComponent } from './classroom-student-create.component';

describe('ClassroomStudentCreateComponent', () => {
  let component: ClassroomStudentCreateComponent;
  let fixture: ComponentFixture<ClassroomStudentCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClassroomStudentCreateComponent]
    });
    fixture = TestBed.createComponent(ClassroomStudentCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
