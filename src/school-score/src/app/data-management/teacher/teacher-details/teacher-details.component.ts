import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { TeacherService } from '../../../services/teacher.service';
import { AccountService } from 'src/app/services/account.service';
import { ThDateService } from 'src/app/services/th-date.service';

@Component({
  selector: 'app-teacher-details',
  templateUrl: './teacher-details.component.html',
  styleUrls: ['./teacher-details.component.css']
})
export class TeacherDetailsComponent implements OnInit {
  data: any;
  data_account: any;
  id: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private accountService: AccountService,
    private teacherService: TeacherService,
    public thDateService: ThDateService,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getServerData();
      this.getServerData_account();
    });
  }

  async getServerData() {
    this.data = await this.teacherService.Get(this.id);
  }

  async getServerData_account() {
    this.data_account = await this.accountService.GetWithTeacher(this.id);
  }

  async delete() {
    if (!confirm(`ยืนยันการลบครู ${this.data.Name} !!!`)) { return; }
    await this.teacherService.Delete(this.id)
      .then((response) => this.router.navigate(['/person-manage']));
  }
}
