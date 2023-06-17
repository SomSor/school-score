import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomDetailsComponent } from './classroom-details.component';

describe('ClassroomDetailsComponent', () => {
  let component: ClassroomDetailsComponent;
  let fixture: ComponentFixture<ClassroomDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClassroomDetailsComponent]
    });
    fixture = TestBed.createComponent(ClassroomDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
