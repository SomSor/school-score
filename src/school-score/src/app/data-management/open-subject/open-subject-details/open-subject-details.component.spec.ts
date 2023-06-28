import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenSubjectDetailsComponent } from './open-subject-details.component';

describe('OpenSubjectDetailsComponent', () => {
  let component: OpenSubjectDetailsComponent;
  let fixture: ComponentFixture<OpenSubjectDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OpenSubjectDetailsComponent]
    });
    fixture = TestBed.createComponent(OpenSubjectDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
