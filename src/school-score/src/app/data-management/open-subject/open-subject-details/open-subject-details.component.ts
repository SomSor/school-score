import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { OpenSubjectService } from 'src/app/services/open-subject.service';

@Component({
  selector: 'app-open-subject-details',
  templateUrl: './open-subject-details.component.html',
  styleUrls: ['./open-subject-details.component.css']
})
export class OpenSubjectDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private openSubjectService: OpenSubjectService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.openSubjectService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบวิชาที่เปิดสอน ${this.data.Name} !!!`)) { return; }
    await this.openSubjectService.Delete(this.id)
      .then((response) => this.router.navigate(['/data-management']));
  }
}
