import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearningAreaDetailsComponent } from './learning-area-details.component';

describe('LearningAreaDetailsComponent', () => {
  let component: LearningAreaDetailsComponent;
  let fixture: ComponentFixture<LearningAreaDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LearningAreaDetailsComponent]
    });
    fixture = TestBed.createComponent(LearningAreaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
