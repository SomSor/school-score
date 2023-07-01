import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { ClassroomService } from 'src/app/services/classroom.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-classroom-details',
  templateUrl: './classroom-details.component.html',
  styleUrls: ['./classroom-details.component.css']
})
export class ClassroomDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private classroomService: ClassroomService,
    private classroomTierService: ClassroomTierService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.classroomService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบวิชา ${this.data.Name} !!!`)) { return; }
    await this.classroomService.Delete(this.id)
      .then((response) => this.router.navigate(['/classroom-manage']));
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }
}
