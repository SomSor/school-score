import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonManageComponent } from './person-manage.component';

describe('PersonManageComponent', () => {
  let component: PersonManageComponent;
  let fixture: ComponentFixture<PersonManageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PersonManageComponent]
    });
    fixture = TestBed.createComponent(PersonManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
