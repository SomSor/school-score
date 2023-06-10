import { TestBed } from '@angular/core/testing';

import { LearningAreaService } from './learning-area.service';

describe('LearningAreaService', () => {
  let service: LearningAreaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LearningAreaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
