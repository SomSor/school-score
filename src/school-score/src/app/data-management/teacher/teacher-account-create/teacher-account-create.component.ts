import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as bcrypt from 'bcryptjs';

import { PagingModel } from '../../../models/paging';
import { AccountService } from 'src/app/services/account.service';
@Component({
  selector: 'app-teacher-account-create',
  templateUrl: './teacher-account-create.component.html',
  styleUrls: ['./teacher-account-create.component.css']
})
export class TeacherAccountCreateComponent {
  data: any;
  fg: FormGroup;
  editingId: any;
  roles = {
    'Teacher': 'ครู',
    'Mod': 'ผู้ช่วย ผู้ดูแลระบบ',
    'Admin': 'ผู้ดูแลระบบ',
  };

  data_learningArea = new PagingModel;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router,
    private accountService: AccountService) {
    this.fg = this.fb.group({
      "PersonId": [null, Validators.required],
      "Username": [null, Validators.required],
      "Password": [null],
      "Roles": [['Teacher'], Validators.required],
    });
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params: any) => {
      this.editingId = params['id'];
      this.getServerData();
    });
  }

  async getServerData() {
    let response = await this.accountService.GetWithTeacherForCreate(this.editingId);
    this.fg.patchValue(response);
    this.data = response.Teacher;
  }

  async onSave() {
    const hashedPassword = bcrypt.hashSync(this.fg.value.Password, 10);
    this.fg.patchValue({
      PersonId: this.editingId,
    });

    if (this.fg.valid) {
      this.accountService.Create({
        PersonId: this.editingId,
        Username: this.fg.value.Username,
        Password: hashedPassword,
        Roles: this.fg.value.Roles,
      }).then(response => this.router.navigate(['/teacher-details'], { queryParams: { id: this.editingId } }));
    }
  }

  getRole() {

  }

}
