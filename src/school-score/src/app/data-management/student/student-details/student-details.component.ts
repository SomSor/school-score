import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { StudentService } from 'src/app/services/student.service';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.css']
})
export class StudentDetailsComponent implements OnInit {
  data: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private studentService: StudentService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    this.data = await this.studentService.Get(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบนักเรียน ${this.data.Name} !!!`)) { return; }
    await this.studentService.Delete(this.id)
      .then((response) => this.router.navigate(['/data-management']));
  }
}
