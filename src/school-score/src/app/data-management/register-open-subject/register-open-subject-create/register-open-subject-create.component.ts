import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from '../../../models/paging';
import { ClassroomService } from 'src/app/services/classroom.service';
import { ClassroomStudentService } from '../../../services/classroom-student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';
import { OpenSubjectService } from 'src/app/services/open-subject.service';

@Component({
  selector: 'app-register-open-subject-create',
  templateUrl: './register-open-subject-create.component.html',
  styleUrls: ['./register-open-subject-create.component.css']
})
export class RegisterOpenSubjectCreateComponent {
  fg: FormGroup;
  editingId: any;

  data_classroom = new PagingModel;
  data_openSubject = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private classroomStudentService: ClassroomStudentService,
    private classroomService: ClassroomService,
    private openSubjectService: OpenSubjectService,
    private classroomTierService: ClassroomTierService) {
    this.fg = this.fb.group({
      "ClassroomId": [null, Validators.required],
      "OpenSubjectId": [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getClassroomData();
    this.getOpenSubjectData();

    this.activatedRoute.queryParams.subscribe(params => {
      this.editingId = params['id'];
      if (this.editingId) {
        this.getServerData();
      }
    });
  }

  async getServerData() {
    let response = await this.classroomStudentService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  async getClassroomData() {
    this.data_classroom = await this.classroomService.GetAll(this.data_classroom.SearchText);
  }

  async getOpenSubjectData() {
    this.data_openSubject = await this.openSubjectService.GetAll(this.data_openSubject.SearchText);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.editingId) {
        this.classroomStudentService.RegisterOpenSubjects(this.fg.value)
          .then(response => this.router.navigate(['/classroomStudent-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.classroomStudentService.RegisterOpenSubjects(this.fg.value)
          .then(response => this.router.navigate(['/data-management']));
      }
    }
  }

  GetTier(tier: any) {
    return this.classroomTierService.GetAbb(tier);
  }

}
