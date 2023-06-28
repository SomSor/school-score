import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenSubjectCreateComponent } from './open-subject-create.component';

describe('OpenSubjectCreateComponent', () => {
  let component: OpenSubjectCreateComponent;
  let fixture: ComponentFixture<OpenSubjectCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OpenSubjectCreateComponent]
    });
    fixture = TestBed.createComponent(OpenSubjectCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
