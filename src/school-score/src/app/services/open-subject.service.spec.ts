import { TestBed } from '@angular/core/testing';

import { OpenSubjectService } from './open-subject.service';

describe('OpenSubjectService', () => {
  let service: OpenSubjectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OpenSubjectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
