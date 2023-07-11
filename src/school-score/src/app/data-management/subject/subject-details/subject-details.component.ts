import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { SubjectService } from '../../../services/subject.service';

@Component({
  selector: 'app-subject-details',
  templateUrl: './subject-details.component.html',
  styleUrls: ['./subject-details.component.css']
})
export class SubjectDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private subjectService: SubjectService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.subjectService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบวิชา ${this.data.Name} !!!`)) { return; }
    await this.subjectService.Delete(this.id)
      .then((response) => this.router.navigate(['/subject-manage']));
  }
}
