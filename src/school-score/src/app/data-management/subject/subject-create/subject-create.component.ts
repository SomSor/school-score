import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from '../../../models/paging';
import { LearningAreaService } from '../../../services/learning-area.service';
import { SubjectService } from '../../../services/subject.service';

@Component({
  selector: 'app-subject-create',
  templateUrl: './subject-create.component.html',
  styleUrls: ['./subject-create.component.css']
})
export class SubjectCreateComponent{
  fg: FormGroup;
  editingId: any;

  data_learningArea = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private subjectService: SubjectService,
    private learningAreaService: LearningAreaService) {
    this.fg = this.fb.group({
      "Code": [null, Validators.required],
      "Name": [null, Validators.required],
      "Description": [null],
      "LearningAreaId": [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getLearingAreaData();

    this.activatedRoute.queryParams.subscribe(params => {
      this.editingId = params['id'];
      if (this.editingId) {
        this.getServerData();
      }
    });
  }

  async getServerData() {
    let response = await this.subjectService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  async getLearingAreaData() {
    this.data_learningArea = await this.learningAreaService.GetAll(this.data_learningArea.SearchText);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.editingId) {
        this.subjectService.Replace(this.editingId, this.fg.value)
          .then(response => this.router.navigate(['/subject-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.subjectService.Create(this.fg.value)
          .then(response => this.router.navigate(['/data-management']));
      }
    }
  }

}
