import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { SchoolYearService } from 'src/app/services/school-year.service';
import { ThDateService } from 'src/app/services/th-date.service';

@Component({
  selector: 'app-school-year-details',
  templateUrl: './school-year-details.component.html',
  styleUrls: ['./school-year-details.component.css']
})
export class SchoolYearDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private schoolYearService: SchoolYearService,
    public thDateService: ThDateService,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.schoolYearService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบปีการศึกษา ${this.data.Name} !!!`)) { return; }
    await this.schoolYearService.Delete(this.id)
      .then((response) => this.router.navigate(['/configuration']));
  }

  async setCurrent() {
    if (!confirm(`ยืนยันการตั้งค่า ${this.data.Year}/${this.data.Semester} เป็นภาคเรียนปัจจุบัน  !!!`)) { return; }
    await this.schoolYearService.SetCurrent(this.id)
      .then((response) => this.router.navigate(['/configuration']));
  }

  isCurrentSemester(): any {
    return this.data.Current.ActivatedDate == this.data.ActivatedDate;
  }

}