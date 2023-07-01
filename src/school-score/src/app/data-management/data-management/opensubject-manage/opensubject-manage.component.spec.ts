import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpensubjectManageComponent } from './opensubject-manage.component';

describe('OpensubjectManageComponent', () => {
  let component: OpensubjectManageComponent;
  let fixture: ComponentFixture<OpensubjectManageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OpensubjectManageComponent]
    });
    fixture = TestBed.createComponent(OpensubjectManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
