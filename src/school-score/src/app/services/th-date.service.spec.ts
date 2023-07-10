import { TestBed } from '@angular/core/testing';

import { ThDateService } from './th-date.service';

describe('ThDateService', () => {
  let service: ThDateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ThDateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
