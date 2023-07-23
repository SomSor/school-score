import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherAccountCreateComponent } from './teacher-account-create.component';

describe('TeacherAccountCreateComponent', () => {
  let component: TeacherAccountCreateComponent;
  let fixture: ComponentFixture<TeacherAccountCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TeacherAccountCreateComponent]
    });
    fixture = TestBed.createComponent(TeacherAccountCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
