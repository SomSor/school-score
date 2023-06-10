import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel } from '../../models/paging';
import { LearningAreaService } from '../../services/learning-area.service';
import { SubjectService } from '../../services/subject.service';

@Component({
  selector: 'app-data-management',
  templateUrl: './data-management.component.html',
  styleUrls: ['./data-management.component.css']
})
export class DataManagementComponent {

  data_learningArea = new PagingModel;
  data_subject = new PagingModel;

  constructor(
    private learningAreaService: LearningAreaService,
    private subjectService: SubjectService,
    ) { }

  ngOnInit(): void {
    this.getLearingAreaData(undefined);
    this.getSubjectData(undefined);
  }

  async getLearingAreaData(event?: PageEvent) {
    this.data_learningArea = await this.learningAreaService.Gets(this.data_learningArea.SearchText, event);
    return event;
  }

  async getSubjectData(event?: PageEvent) {
    this.data_subject = await this.subjectService.Gets(this.data_subject.SearchText, event);
    return event;
  }

}
