import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from 'src/app/models/paging';
import { ClassroomService } from '../../../services/classroom.service';
import { TeacherService } from 'src/app/services/teacher.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-classroom-create',
  templateUrl: './classroom-create.component.html',
  styleUrls: ['./classroom-create.component.css']
})
export class ClassroomCreateComponent {
  fg: FormGroup;
  editingId: any;

  data_teacher = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private classroomService: ClassroomService,
    private teacherService: TeacherService,
    private classroomTierService: ClassroomTierService) {
    this.fg = this.fb.group({
      "Tier": [null, Validators.required],
      "ClassYear": [null, Validators.required],
      "Subclass": [null, Validators.required],
      "TeacherId": [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getTeacherData();

    this.activatedRoute.queryParams.subscribe(params => {
      this.editingId = params['id'];
      if (this.editingId) {
        this.getServerData();
      }
    });
  }

  async getServerData() {
    let response = await this.classroomService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  async getTeacherData() {
    this.data_teacher = await this.teacherService.GetAll(this.data_teacher.SearchText);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.editingId) {
        this.classroomService.Replace(this.editingId, this.fg.value)
          .then(response => this.router.navigate(['/classroom-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.classroomService.Create(this.fg.value)
          .then(response => this.router.navigate(['/classroom-management']));
      }
    }
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

}
