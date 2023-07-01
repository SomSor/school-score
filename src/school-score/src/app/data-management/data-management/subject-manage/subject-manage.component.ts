import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel, RegisteredOpenSubject } from '../../../models/paging';
import { LearningAreaService } from 'src/app/services/learning-area.service';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-subject-manage',
  templateUrl: './subject-manage.component.html',
  styleUrls: ['./subject-manage.component.css']
})
export class SubjectManageComponent {

  data_learningArea = new PagingModel;
  data_subject = new PagingModel;
  data_registerOpenSubject = new RegisteredOpenSubject;

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
