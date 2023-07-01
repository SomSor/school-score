import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomManageComponent } from './classroom-manage.component';

describe('ClassroomManageComponent', () => {
  let component: ClassroomManageComponent;
  let fixture: ComponentFixture<ClassroomManageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClassroomManageComponent]
    });
    fixture = TestBed.createComponent(ClassroomManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
