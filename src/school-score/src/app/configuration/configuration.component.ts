import { ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { SchoolYearPaging } from '../models/paging';
import { SchoolService } from '../services/school.service';
import { SchoolYearService } from '../services/school-year.service';
import { ThDateService } from '../services/th-date.service';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent {

  data_school: any;
  data_schoolYear = new SchoolYearPaging;

  constructor(private activatedRoute: ActivatedRoute,
    private schoolService: SchoolService,
    private schoolYearService: SchoolYearService,
    public thDateService: ThDateService,
  ) {
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.getServerData_school(undefined);
      this.getServerData_schoolYear(undefined);
    });
  }

  async getServerData_school(event?: PageEvent) {
    this.data_school = await this.schoolService.Current();
  }

  async getServerData_schoolYear(event?: PageEvent) {
    this.data_schoolYear = await this.schoolYearService.GetsWithCurrent(null);
  }

  isCurrentSemester(item: any): any {
    return this.data_schoolYear.Current.ActivatedDate == item.ActivatedDate;
  }

}
