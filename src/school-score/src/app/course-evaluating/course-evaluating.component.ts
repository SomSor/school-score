import { ActivatedRoute } from '@angular/router';
import { Component, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ClassroomStudentService } from '../services/classroom-student.service';
import { ClassroomTierService } from '../helpers/classroom-tier/classroom-tier.service';

@Component({
  selector: 'app-course-evaluating',
  templateUrl: './course-evaluating.component.html',
  styleUrls: ['./course-evaluating.component.css']
})
export class CourseEvaluatingComponent {

  fg: FormGroup;
  data: any;
  dataEvaluate: any;
  @Input()
  classroomId: any;
  @Input()
  opensubjectId: any;
  @Input()
  scoringGroupId: any;

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
      this.scoringGroupId = params['scoringgroupid'];
      this.fg.value.OpenSubjectId = this.opensubjectId;
      this.fg.value.ClassroomId = this.classroomId;

      this.getServerData(undefined);
    });
  }

  async getServerData(event?: PageEvent) {
    this.data = null;
    // this.dataEvaluate = null;
    this.data = await this.classroomStudentService.getClassroomOpenSubjectData(this.classroomId, this.opensubjectId);
    this.dataEvaluate = this.data.OpenSubject.Evaluates.filter((x: any) => x.Id == this.scoringGroupId)[0];

    this.fg.value.ClassroomStudentScores = [];
    this.fg.value.ClassroomStudentRemarks = [];

    this.data.Data.forEach((cs: any) => {
      cs.RegisterOpenSubjects.filter((x: any) => x.OpenSubjectId == this.opensubjectId).forEach((ros: any) => {
        ros.EvaluateResults.filter((x: any) => x.ScoringGroupId == this.scoringGroupId).forEach((ev: any) => {
          ev.ScoringSubGroupResults.forEach((sgr: any) => {
            sgr.ScoringResults.forEach((sr: any) => {
              this.fg.value.ClassroomStudentScores.push({
                StudentId: cs.StudentId,
                ScoringGroupId: ev.ScoringGroupId,
                ScoringSubGroupId: sgr.ScoringSubGroupId,
                ScoringId: sr.ScoringId,
                Score: sr.Score,
              });
            });
          });
          this.fg.value.ClassroomStudentRemarks.push({
            StudentId: cs.StudentId,
            ScoringGroupId: this.scoringGroupId,
            Remark: ev.Remark,
          });
        });
      });
    });
  }

  getScore(studentId: any, scoringId: any): any {
    let scoringResult = this.fg.value.ClassroomStudentScores.filter((x: any) => x.StudentId == studentId && x.ScoringId == scoringId)[0];
    return scoringResult.Score ?? 0;
  }

  getRemark(studentId: any): any {
    let scoringResult = this.fg.value.ClassroomStudentRemarks.filter((x: any) => x.StudentId == studentId)[0];
    return scoringResult.Remark;
  }

  ScoreChange(studentId: any, scoringId: any, event: any) {
    var data = this.fg.value.ClassroomStudentScores.filter((x: any) => x.StudentId == studentId && x.ScoringId == scoringId)[0];
    data.Score = parseFloat(event);
  }

  RemarkChange(studentId: any, event: any) {
    var data = this.fg.value.ClassroomStudentRemarks.filter((x: any) => x.StudentId == studentId)[0];
    data.Remark = event.target.value;
  }

  checkScoreChange(studentId: any, scoringSubGroupId: any, scoringId: any): any {
    var inputScore = this.fg.value.ClassroomStudentScores.filter((x: any) =>
      x.StudentId == studentId &&
      x.ScoringId == scoringId)[0].Score;

    let studentClassroom = this.data.Data.filter((cs: any) => cs.StudentId == studentId)[0];
    let registerOpenSubject = studentClassroom.RegisterOpenSubjects.filter((x: any) => x.OpenSubjectId == this.opensubjectId)[0];
    let evaluateResult = registerOpenSubject.EvaluateResults.filter((x: any) => x.ScoringGroupId == this.scoringGroupId)[0]
    let scoringSubGroupResult = evaluateResult.ScoringSubGroupResults.filter((x: any) => x.ScoringSubGroupId == scoringSubGroupId)[0]
    let scoringResult = scoringSubGroupResult.ScoringResults.filter((x: any) => x.ScoringId == scoringId)[0];
    let score = scoringResult.Score

    return score != inputScore;
  }

  checkRemarkChange(studentId: any): any {
    var inputRemark = this.fg.value.ClassroomStudentRemarks.filter((x: any) => x.StudentId == studentId)[0].Remark;

    let studentClassroom = this.data.Data.filter((cs: any) => cs.StudentId == studentId)[0];
    let registerOpenSubject = studentClassroom.RegisterOpenSubjects.filter((x: any) => x.OpenSubjectId == this.opensubjectId)[0];
    let scoringGroup = registerOpenSubject.EvaluateResults.filter((x: any) => x.ScoringGroupId == this.scoringGroupId)[0];
    let remark = scoringGroup.Remark

    return remark != inputRemark;
  }

  SumScore(studentId: any): any {
    return this.fg.value.ClassroomStudentScores
      .filter(((x: any) => x.StudentId == studentId))
      .map((x: any) => x.Score).reduce((sum: any, current: any) => sum + current);
  }

  GetGrade(studentId: any) {
    let score = this.SumScore(studentId);
    let grade = 0;
    for (let i = 0; i < this.dataEvaluate.GradingCriterias.length; i++) {
      if (score >= this.dataEvaluate.GradingCriterias[i].Score) {
        grade = this.dataEvaluate.GradingCriterias[i].Grade;
        break;
      }
    }

    return grade;
  }

  GetTier(tier: any) {
    return this.classroomTierService.Get(tier);
  }

  OnSave() {
    if (!confirm("ยืนยันการบันทึกคะแนน")) return;
    this.classroomStudentService.SaveEvaluate(this.fg.value)
      .then(response => this.getServerData(undefined));
  }

  getArrayFromNumber(number: any): any {
    let numberArray = [];
    for (var j = 0; j < number; j++) {
      numberArray.push("Col" + (j + 1));
    }
    return numberArray;
  }

}
