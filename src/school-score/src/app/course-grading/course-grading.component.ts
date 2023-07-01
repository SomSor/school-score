import { ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ClassroomStudentService } from '../services/classroom-student.service';
import { ClassroomTierService } from '../helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-course-grading',
  templateUrl: './course-grading.component.html',
  styleUrls: ['./course-grading.component.css']
})
export class CourseGradingComponent {

  fg: FormGroup;
  data: any;
  classroomId: any;
  opensubjectId: any;

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute,
    private classroomStudentService: ClassroomStudentService,
    private classroomTierService: ClassroomTierService,
  ) {
    this.fg = this.fb.group({
      "ClassroomId": [null, Validators.required],
      "OpenSubjectId": [null, Validators.required],
      "ClassroomStudentScores": [[], Validators.required],
      "ClassroomStudentRemarks": [[], Validators.required],
    });
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
    this.data = await this.classroomStudentService.getClassroomOpenSubjectData(this.classroomId, this.opensubjectId);

    this.fg.value.ClassroomStudentScores = [];
    this.fg.value.ClassroomStudentRemarks = [];

    this.data.Data.forEach((cs: any) => {
      cs.RegisterOpenSubjects.filter((x: any) => x.OpenSubjectId == this.opensubjectId).forEach((ros: any) => {
        ros.ExamResult.ScoringSubGroupResults.forEach((sgr: any) => {
          sgr.ScoringResults.forEach((sr: any) => {
            this.fg.value.ClassroomStudentScores.push({
              StudentId: cs.StudentId,
              ScoringSubGroupId: sgr.ScoringSubGroupId,
              ScoringId: sr.ScoringId,
              Score: sr.Score,
            });
          });
        });
        this.fg.value.ClassroomStudentRemarks.push({
          StudentId: cs.StudentId,
          ScoringGroupId: ros.ExamResult.ScoringGroupId,
          Remark: ros.ExamResult.Remark,
        });
      });
    });
    console.log(this.data);
    
  }

  getScore(studentId: any, scoringId: any): any {
    let classroomStudentScore = this.fg.value.ClassroomStudentScores.filter((x: any) => x.StudentId == studentId && x.ScoringId == scoringId)[0];
    return classroomStudentScore.Score ?? 0;
  }

  getRemark(studentId: any): any {
    let classroomStudentScore = this.fg.value.ClassroomStudentRemarks.filter((x: any) => x.StudentId == studentId)[0];
    return classroomStudentScore.Remark;
  }

  IsPass(studentId: any): any {
    let score = this.SumScore(studentId);
    return score >= 50;
  }

  IsPassFixed(studentId: any): any {
    let score = this.SumScore(studentId, true);
    return score >= 50;
  }

  ScoreChange(studentId: any, scoringSubGroupId: any, scoringId: any, event: any) {
    var classroomStudent = this.fg.value.ClassroomStudentScores.filter((x: any) => x.StudentId == studentId
      && x.ScoringSubGroupId == scoringSubGroupId
      && x.ScoringId == scoringId)[0];
    classroomStudent.Score = parseFloat(event.target.value);
  }

  RemarkChange(studentId: any, event: any) {
    var data = this.fg.value.ClassroomStudentRemarks.filter((x: any) => x.StudentId == studentId)[0];
    data.Remark = event.target.value;
  }

  checkScoreChange(studentId: any, scoringSubGroupId: any, scoringId: any): any {
    var inputScore = this.fg.value.ClassroomStudentScores.filter((x: any) => x.StudentId == studentId
      && x.ScoringSubGroupId == scoringSubGroupId
      && x.ScoringId == scoringId)[0].Score;

    let score = this.data.Data.filter((cs: any) => cs.StudentId == studentId)[0]
      .RegisterOpenSubjects.filter((ros: any) => ros.OpenSubjectId == this.opensubjectId)[0]
      .ExamResult.ScoringSubGroupResults.filter((sgr: any) => sgr.ScoringSubGroupId == scoringSubGroupId)[0]
      .ScoringResults.filter((sr: any) => sr.ScoringId == scoringId)[0].Score;

    return score != inputScore;
  }

  checkScoreChangeError(studentId: any, scoringSubGroupId: any, scoringId: any): any {
    var inputScore = this.fg.value.ClassroomStudentScores.filter((x: any) => x.StudentId == studentId
      && x.ScoringSubGroupId == scoringSubGroupId
      && x.ScoringId == scoringId)[0].Score;

    let maxScore = this.data.OpenSubject.Exam.ScoringSubGroups.filter((ssg: any) => ssg.Id == scoringSubGroupId)[0]
      .Scorings.filter((s: any) => s.Id == scoringId)[0].MaxScore;

    return inputScore < 0 || inputScore > maxScore;
  }

  checkRemarkChange(studentId: any): any {
    var inputRemark = this.fg.value.ClassroomStudentRemarks.filter((x: any) => x.StudentId == studentId)[0].Remark;

    let studentClassroom = this.data.Data.filter((cs: any) => cs.StudentId == studentId)[0];
    let registerOpenSubject = studentClassroom.RegisterOpenSubjects.filter((x: any) => x.OpenSubjectId == this.opensubjectId)[0];
    let remark = registerOpenSubject.ExamResult.Remark

    return remark != inputRemark;
  }

  SumScore(studentId: any, isFixed: any = false): any {
    let scoringSubGroupId = this.data.OpenSubject.Exam.ScoringSubGroups[0].Id;
    if (isFixed) scoringSubGroupId = this.data.OpenSubject.Exam.ScoringSubGroups[1].Id;
    return this.fg.value.ClassroomStudentScores
      .filter(((x: any) => x.StudentId == studentId && x.ScoringSubGroupId == scoringSubGroupId))
      .map((x: any) => x.Score).reduce((sum: any, current: any) => sum + current);
  }

  GetGrade(studentId: any) {
    let score = this.SumScore(studentId);
    let grade = 0;
    for (let i = 0; i < this.data.OpenSubject.Exam.GradingCriterias.length; i++) {
      if (score >= this.data.OpenSubject.Exam.GradingCriterias[i].Score) {
        grade = this.data.OpenSubject.Exam.GradingCriterias[i].Grade;
        break;
      }
    }

    return grade;
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

  maxSumScore(scorings: any): any {
    let sumScore = scorings.map((x: any) => x.MaxScore).reduce((sum: any, current: any) => sum + current);
    return sumScore;
  }

  OnSave() {
    if (!confirm("ยืนยันการบันทึกคะแนน")) return;
    this.classroomStudentService.SaveExam(this.fg.value)
      .then(response => this.getServerData(undefined));
  }

}
