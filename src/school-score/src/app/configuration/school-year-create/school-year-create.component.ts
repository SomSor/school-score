import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { PagingModel } from 'src/app/models/paging';
import { SchoolYearService } from 'src/app/services/school-year.service';

@Component({
  selector: 'app-school-year-create',
  templateUrl: './school-year-create.component.html',
  styleUrls: ['./school-year-create.component.css']
})
export class SchoolYearCreateComponent {
  fg: FormGroup;
  editingId: any;

  data_teacher = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private schoolYearService: SchoolYearService) {
    this.fg = this.fb.group({
      "Year": [new Date().getFullYear() + 543, Validators.required],
      "Semester": [1, Validators.required],
      "StartDate": [new Date(), Validators.required],
      "EndDate": [new Date(), Validators.required],
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
    let response = await this.schoolYearService.Get(this.editingId);
    this.fg.patchValue(response);
  }

  onSave() {
    if (this.fg.valid) {
      this.fg.value.Year = this.fg.value.Year.toString();
      if (this.editingId) {
        this.schoolYearService.Replace(this.editingId, this.fg.value)
          .then(response => this.router.navigate(['/schoolyear-details'], { queryParams: { id: this.editingId } }));
      } else {
        this.schoolYearService.Create(this.fg.value)
          .then(response => this.router.navigate(['/configuration']));
      }
    }
  }

}
