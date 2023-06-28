import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from '../../../models/paging';
import { OpenSubjectService } from 'src/app/services/open-subject.service';
import { SubjectService } from 'src/app/services/subject.service';
import { TeacherService } from 'src/app/services/teacher.service';

@Component({
  selector: 'app-open-subject-create',
  templateUrl: './open-subject-create.component.html',
  styleUrls: ['./open-subject-create.component.css']
})
export class OpenSubjectCreateComponent {
  fg: FormGroup;
  editingId: any;

  data_learningArea = new PagingModel;
  data_subject = new PagingModel;
  data_teacher = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private openSubjectService: OpenSubjectService,
    private subjectService: SubjectService,
    private teacherService: TeacherService) {
    this.fg = this.fb.group({
      "Semester": [null, Validators.required],
      "SubjectId": [null, Validators.required],
      "MainTeacherId": [null, Validators.required],
      "MidTermMaxScore": [null, Validators.required],
      "FinalMaxScore": [null, Validators.required],
      "Description": [null],
    });
  }

  ngOnInit(): void {
    this.getSubjectData();
    this.getTeacherData();

    this.activatedRoute.queryParams.subscribe(params => {
      this.editingId = params['id'];
      if (this.editingId) {
        this.getServerData();
      }
    });
  }

  async getServerData() {
    let response = await this.openSubjectService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  async getSubjectData() {
    this.data_subject = await this.subjectService.GetAll(this.data_subject.SearchText);
  }

  async getTeacherData() {
    this.data_teacher = await this.teacherService.GetAll(this.data_teacher.SearchText);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.fg.value["MidTermMaxScore"] + this.fg.value["FinalMaxScore"] != 100) {
        alert("ผลรวม คะแนนเต็มกลางภาคเรียน และ คะแนนเต็มปลายภาคเรียน ต้องเป็น 100 คะแนน");
        return;
      }
      if (this.editingId) {
        this.openSubjectService.Replace(this.editingId, this.fg.value)
          .then(response => this.router.navigate(['/open-subject-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.openSubjectService.Create(this.fg.value)
          .then(response => this.router.navigate(['/data-management']));
      }
    }
  }

}
