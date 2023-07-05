import { ActivatedRoute } from '@angular/router';
import { Component, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ClassroomStudentService } from '../services/classroom-student.service';
import { ClassroomTierService } from '../helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-course-check',
  templateUrl: './course-check.component.html',
  styleUrls: ['./course-check.component.css']
})
export class CourseCheckComponent {

  dayOfWeeks: any = ["อา.", "จ.", "อ.", "พ.", "พฤ.", "ศ.", "ส."];
  monthOfYears: any = ["ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค."];
  fg: FormGroup;
  data: any;

  dataTimeTables: any;
  dataWeeks: any = [];
  dataMonths: any = [];

  @Input()
  classroomId: any;
  @Input()
  opensubjectId: any;
  showMonth: any;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute,
    private classroomStudentService: ClassroomStudentService,
    private classroomTierService: ClassroomTierService,
  ) {
    this.fg = this.fb.group({
      "ClassroomId": [null, Validators.required],
      "OpenSubjectId": [null, Validators.required],
      "ClassroomStudentChecks": [[], Validators.required],
    });
    this.showMonth = new Date().getMonth();
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.classroomId = params['classroomid'];
      this.opensubjectId = params['opensubjectid'];
      this.fg.value.OpenSubjectId = this.opensubjectId;
      this.fg.value.ClassroomId = this.classroomId;

      this.getServerData(undefined);
    });
  }

  async getServerData(event?: PageEvent) {
    this.data = await this.classroomStudentService.getClassroomOpenSubjectTimetableData(this.classroomId, this.opensubjectId);

    this.fg.value.ClassroomStudentChecks = [];
    this.dataTimeTables = [];
    this.dataWeeks = [];
    this.dataMonths = [];

    this.data.TimeTables.forEach((x: any) => {
      if (this.dataMonths.filter((y: any) => y.Month == x.Month).length == 0) this.dataMonths.push({ Month: x.Month, DayCount: 1 });
      else this.dataMonths.filter((y: any) => y.Month == x.Month)[0].DayCount++;
    });

    this.dataTimeTables = this.data.TimeTables.filter((x: any) => x.Month == this.showMonth + 1 /*&& x.DayNo != 0 && x.DayNo != 6*/);

    this.dataTimeTables.forEach((x: any) => {
      if (this.dataWeeks.filter((y: any) => y.Week == x.Week).length == 0) this.dataWeeks.push({ Week: x.Week, DayCount: 1 });
      else this.dataWeeks.filter((y: any) => y.Week == x.Week)[0].DayCount++;
    });

    this.data.TimeTables.filter((tt: any) => this.data.OpenSubject.TimeTables.filter((ostt: any) => ostt[0] == tt.DayNo).length > 0).forEach((tt: any) => {
      this.data.Data.forEach((cs: any) => {
        cs.RegisterOpenSubjects.filter((ros: any) => ros.OpenSubjectId == this.opensubjectId).forEach((ros: any) => {
          let timeTableKey = this.data.OpenSubject.TimeTables.filter((att: any) => Array.from(att)[0] == tt.DayNo)[0];
          this.fg.value.ClassroomStudentChecks.push({
            StudentId: cs.StudentId,
            Date: tt.Date,
            TimeTableKey: timeTableKey,
            IsPresent: ros.Attendances?.filter((att: any) => att.StampDate == tt.Date && Array.from(att.TimeTableKey)[0] == tt.DayNo).length > 0 ?? false,
          });
        });
      });
    });
  }

  selectMonth(month: any) {
    if (this.showMonth != month - 1) {
      this.showMonth = month - 1;
      this.getServerData(undefined);
    }
  }

  DayChackChange(dayNo: any, date: any, event: any) {
    this.fg.value.ClassroomStudentChecks.filter((cs: any) => Array.from(cs.TimeTableKey)[0] == dayNo && cs.Date == date).forEach((cs: any) => {
      this.ChackChange(cs.StudentId, dayNo, date, event)
    });
  }

  GetIsPresent(studentId: any, dayNo: any, date: any): any {
    return this.fg.value.ClassroomStudentChecks.filter((cs: any) => cs.StudentId == studentId && Array.from(cs.TimeTableKey)[0] == dayNo && cs.Date == date)[0];
  }

  ChackChange(studentId: any, dayNo: any, date: any, event: any) {
    var classroomStudent = this.fg.value.ClassroomStudentChecks.filter((cs: any) => cs.StudentId == studentId && Array.from(cs.TimeTableKey)[0] == dayNo && cs.Date == date)[0];
    classroomStudent.IsPresent = event.target.checked;
  }

  checkIsPresentChange(studentId: any, dayNo: any, date: any): any {
    var inputCheck = this.fg.value.ClassroomStudentChecks.filter((cs: any) => cs.StudentId == studentId && Array.from(cs.TimeTableKey)[0] == dayNo && cs.Date == date)[0].IsPresent;

    let isPresent = this.data.Data.filter((cs: any) => cs.StudentId == studentId)[0]
      .RegisterOpenSubjects.filter((ros: any) => ros.OpenSubjectId == this.opensubjectId)[0]
      .Attendances?.filter((att: any) => att.StampDate == date && Array.from(att.TimeTableKey)[0] == dayNo).length > 0 ?? false;

    return isPresent != inputCheck;
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

  sumCheck(studentId: any): any {
    let sumStudentCheck = this.fg.value.ClassroomStudentChecks.filter((cs: any) =>
      cs.StudentId == studentId && cs.IsPresent && this.data.OpenSubject.TimeTables.filter((ostt: any) => ostt == cs.TimeTableKey).length > 0).length;
    return sumStudentCheck;
  }

  sumCheckPercent(studentId: any): any {
    let sumStudentCheck = this.sumCheck(studentId);
    let totalDay = this.data.TimeTables.filter((tt: any) => this.data.OpenSubject.TimeTables.filter((ostt: any) => ostt[0] == tt.DayNo).length > 0).length;

    return (sumStudentCheck * 100) / totalDay;
  }

  isOpen(dayNo: any): any {
    return this.data.OpenSubject.TimeTables.filter((x: any) => x[0] == dayNo).length > 0;
  }

  addHours = (date: Date, hours: number): Date => {
    const result = new Date(date);
    result.setHours(result.getHours() + hours);
    return result;
  };

  THDate(dateStr: any): any {
    let date = new Date(dateStr);
    let thDate = `${date.getDate()} ${this.monthOfYears[date.getMonth()]} ${date.getFullYear() + 543}`;
    return thDate;
  }

  OnSave() {
    if (!confirm("ยืนยันการบันทึกคะแนน")) return;
    this.classroomStudentService.SaveAttendances(this.fg.value)
      .then(response => this.getServerData(undefined));
  }
}
