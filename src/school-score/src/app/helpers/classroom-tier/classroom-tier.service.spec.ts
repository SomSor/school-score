import { TestBed } from '@angular/core/testing';

import { ClassroomTierService } from './classroom-tier.service';

describe('ClassroomTierService', () => {
  let service: ClassroomTierService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClassroomTierService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
