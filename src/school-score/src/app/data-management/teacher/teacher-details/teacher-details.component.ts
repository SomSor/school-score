import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { TeacherService } from '../../../services/teacher.service';

@Component({
  selector: 'app-teacher-details',
  templateUrl: './teacher-details.component.html',
  styleUrls: ['./teacher-details.component.css']
})
export class TeacherDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private teacherService: TeacherService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.teacherService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบครู ${this.data.Name} !!!`)) { return; }
    await this.teacherService.Delete(this.id)
      .then((response) => this.router.navigate(['/person-manage']));
  }
}
