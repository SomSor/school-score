import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { LearningAreaService } from '../../../services/learning-area.service';

@Component({
  selector: 'app-memberdetails',
  templateUrl: './learning-area-details.component.html',
  styleUrls: ['./learning-area-details.component.css']
})
export class LearningAreaDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private learningAreaService: LearningAreaService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.learningAreaService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบกลุ่มสาระการเรียนรู้ ${this.data.Name} !!!`)) { return; }
    await this.learningAreaService.Delete(this.id)
      .then((response) => this.router.navigate(['/data-management']));
  }
}
