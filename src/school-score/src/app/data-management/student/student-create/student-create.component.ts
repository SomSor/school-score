import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from '../../../models/paging';
import { StudentService } from 'src/app/services/student.service';

@Component({
  selector: 'app-student-create',
  templateUrl: './student-create.component.html',
  styleUrls: ['./student-create.component.css']
})
export class StudentCreateComponent {
  fg: FormGroup;
  editingId: any;

  data_learningArea = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private studentService: StudentService) {
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
    let response = await this.studentService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  onSave() {
    if (this.fg.valid) {
      if (this.editingId) {
        this.studentService.Replace(this.editingId, this.fg.value)
          .then(response => this.router.navigate(['/student-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.studentService.Create(this.fg.value)
          .then(response => this.router.navigate(['/data-management']));
      }
    }
  }

}
