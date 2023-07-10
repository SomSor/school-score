import { ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { ClassroomStudentService } from '../services/classroom-student.service';
import { ClassroomTierService } from '../helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-corse-details',
  templateUrl: './corse-details.component.html',
  styleUrls: ['./corse-details.component.css']
})
export class CorseDetailsComponent {
  
  data: any;
  currentPage: any = 'course-check';
  classroomId: any;
  opensubjectId: any;
  scoringGroupId: any;

  constructor(private activatedRoute: ActivatedRoute,
    private classroomStudentService: ClassroomStudentService,
    private classroomTierService: ClassroomTierService,
  ) {
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.currentPage = params['currentPage'];
      this.classroomId = params['classroomid'];
      this.opensubjectId = params['opensubjectid'];
      this.scoringGroupId = params['scoringgroupid'];

      this.getServerData(undefined);
    });
  }

  async getServerData(event?: PageEvent) {
    this.data = null;
    // this.dataEvaluate = null;
    this.data = await this.classroomStudentService.getClassroomOpenSubjectData(this.classroomId, this.opensubjectId);
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

}
