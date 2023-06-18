import { TestBed } from '@angular/core/testing';

import { ClassroomStudentService } from './classroom-student.service';

describe('ClassroomStudentService', () => {
  let service: ClassroomStudentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClassroomStudentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
