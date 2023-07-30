import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from '../../../models/paging';
import { TeacherService } from 'src/app/services/teacher.service';

@Component({
  selector: 'app-teacher-create',
  templateUrl: './teacher-create.component.html',
  styleUrls: ['./teacher-create.component.css']
})
export class TeacherCreateComponent {
  fg: FormGroup;
  editingId: any;

  data_learningArea = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private teacherService: TeacherService) {
    this.fg = this.fb.group({
      "Code": [null, Validators.required],
      "Prefix": [null],
      "Name": [null, Validators.required],
      "Lastname": [null],
      "PID": [null],
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.editingId = params['id'];
      if (this.editingId) {
        this.getServerData();
      }
    });
  }

  async getServerData() {
    let response = await this.teacherService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.editingId) {
        this.teacherService.Replace(this.editingId, this.fg.value, true, null, (() => this.router.navigate(['/teacher-details'], { queryParams: { id: this.editingId } })));
      } else {
        this.teacherService.Create(this.fg.value, true, null, (() => this.router.navigate(['/person-manage'])));
      }
    }
  }

}
