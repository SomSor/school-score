import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterOpenSubjectDetailsComponent } from './register-open-subject-details.component';

describe('RegisterOpenSubjectDetailsComponent', () => {
  let component: RegisterOpenSubjectDetailsComponent;
  let fixture: ComponentFixture<RegisterOpenSubjectDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegisterOpenSubjectDetailsComponent]
    });
    fixture = TestBed.createComponent(RegisterOpenSubjectDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
