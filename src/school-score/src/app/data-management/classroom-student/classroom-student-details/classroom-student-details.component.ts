import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { ClassroomStudentService } from 'src/app/services/classroom-student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-classroom-student-details',
  templateUrl: './classroom-student-details.component.html',
  styleUrls: ['./classroom-student-details.component.css']
})
export class ClassroomStudentDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private classroomStudentService: ClassroomStudentService,
    private classroomTierService: ClassroomTierService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.classroomStudentService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบวิชา ${this.data.Name} !!!`)) { return; }
    await this.classroomStudentService.Delete(this.id)
      .then((response) => this.router.navigate(['/data-management']));
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }
}
