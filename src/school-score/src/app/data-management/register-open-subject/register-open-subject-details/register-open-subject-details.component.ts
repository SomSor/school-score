import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { ClassroomStudentService } from 'src/app/services/classroom-student.service';
import { ClassroomTierService } from 'src/app/helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-register-open-subject-details',
  templateUrl: './register-open-subject-details.component.html',
  styleUrls: ['./register-open-subject-details.component.css']
})
export class RegisterOpenSubjectDetailsComponent implements OnInit {
  data: any;
  id: any;
  subjects: any;
  OpenSubjects: any;

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
    this.classroomStudentService.GetRegisterOpenSubjects(this.id).then(response => {
      this.data = response.Data[0];
      this.subjects = response.Subjects;
      this.OpenSubjects = response.OpenSubjects;
    });
  }

  async delete() {
    if (!confirm(`ยืนยันการลบ ${this.data.Name} !!!`)) { return; }
    await this.classroomStudentService.Delete(this.id)
      .then((response) => this.router.navigate(['/classroom-manage']));
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }
}
