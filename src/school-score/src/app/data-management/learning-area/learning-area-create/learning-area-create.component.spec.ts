import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LearningAreaCreateComponent } from './learning-area-create.component';

describe('LearningAreaCreateComponent', () => {
  let component: LearningAreaCreateComponent;
  let fixture: ComponentFixture<LearningAreaCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LearningAreaCreateComponent]
    });
    fixture = TestBed.createComponent(LearningAreaCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
