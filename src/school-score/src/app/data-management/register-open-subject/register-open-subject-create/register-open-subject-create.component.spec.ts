import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterOpenSubjectCreateComponent } from './register-open-subject-create.component';

describe('RegisterOpenSubjectCreateComponent', () => {
  let component: RegisterOpenSubjectCreateComponent;
  let fixture: ComponentFixture<RegisterOpenSubjectCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegisterOpenSubjectCreateComponent]
    });
    fixture = TestBed.createComponent(RegisterOpenSubjectCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
