import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from '../../../models/paging';
import { ClassroomService } from 'src/app/services/classroom.service';
import { ClassroomStudentService } from '../../../services/classroom-student.service';
import { StudentService } from 'src/app/services/student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-classroom-student-create',
  templateUrl: './classroom-student-create.component.html',
  styleUrls: ['./classroom-student-create.component.css']
})
export class ClassroomStudentCreateComponent {
  fg: FormGroup;
  editingId: any;

  data_classroom = new PagingModel;
  data_student = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private classroomStudentService: ClassroomStudentService,
    private classroomService: ClassroomService,
    private studentService: StudentService,
    private classroomTierService: ClassroomTierService) {
    this.fg = this.fb.group({
      "ClassroomId": [null, Validators.required],
      "StudentId": [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getClassroomData();
    this.getStudentData();

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

  async getStudentData() {
    this.data_student = await this.studentService.GetAll(this.data_student.SearchText);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.editingId) {
        this.classroomStudentService.Replace(this.editingId, this.fg.value)
          .then(response => this.router.navigate(['/classroomStudent-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.classroomStudentService.Create(this.fg.value)
          .then(response => this.router.navigate(['/data-management']));
      }
    }
  }

  GetTier(tier: any) {
    return this.classroomTierService.GetAbb(tier);
  }

}
