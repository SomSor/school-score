import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ReportComponent } from './report/report.component';
import { ConfigurationComponent } from './configuration/configuration.component';

import { CourseComponent } from './course/course.component';
import { CourseCheckComponent } from './course-check/course-check.component';
import { CourseEvaluatingComponent } from './course-evaluating/course-evaluating.component';
import { CourseGradingComponent } from './course-grading/course-grading.component';

import { PersonManageComponent } from './data-management/data-management/person-manage/person-manage.component';
import { TeacherCreateComponent } from './data-management/teacher/teacher-create/teacher-create.component';
import { TeacherDetailsComponent } from './data-management/teacher/teacher-details/teacher-details.component';
import { TeacherAccountCreateComponent } from './data-management/teacher/teacher-account-create/teacher-account-create.component';
import { StudentCreateComponent } from './data-management/student/student-create/student-create.component';
import { StudentDetailsComponent } from './data-management/student/student-details/student-details.component';

import { SubjectManageComponent } from './data-management/data-management/subject-manage/subject-manage.component';
import { LearningAreaCreateComponent } from './data-management/learning-area/learning-area-create/learning-area-create.component';
import { LearningAreaDetailsComponent } from './data-management/learning-area/learning-area-details/learning-area-details.component';
import { SubjectCreateComponent } from './data-management/subject/subject-create/subject-create.component';
import { SubjectDetailsComponent } from './data-management/subject/subject-details/subject-details.component';

import { ClassroomManageComponent } from './data-management/data-management/classroom-manage/classroom-manage.component';
import { ClassroomCreateComponent } from './data-management/classroom/classroom-create/classroom-create.component';
import { ClassroomDetailsComponent } from './data-management/classroom/classroom-details/classroom-details.component';
import { ClassroomStudentCreateComponent } from './data-management/classroom-student/classroom-student-create/classroom-student-create.component';
import { ClassroomStudentDetailsComponent } from './data-management/classroom-student/classroom-student-details/classroom-student-details.component';

import { OpensubjectManageComponent } from './data-management/data-management/opensubject-manage/opensubject-manage.component';
import { OpenSubjectCreateComponent } from './data-management/open-subject/open-subject-create/open-subject-create.component';
import { OpenSubjectDetailsComponent } from './data-management/open-subject/open-subject-details/open-subject-details.component';
import { RegisterOpenSubjectCreateComponent } from './data-management/register-open-subject/register-open-subject-create/register-open-subject-create.component';
import { RegisterOpenSubjectDetailsComponent } from './data-management/register-open-subject/register-open-subject-details/register-open-subject-details.component';
import { CorseDetailsComponent } from './corse-details/corse-details.component';
import { SchoolYearCreateComponent } from './configuration/school-year-create/school-year-create.component';
import { SchoolYearDetailsComponent } from './configuration/school-year-details/school-year-details.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'report', component: ReportComponent },

  { path: 'configuration', component: ConfigurationComponent },
  { path: 'schoolyear-create', component: SchoolYearCreateComponent },
  { path: 'schoolyear-details', component: SchoolYearDetailsComponent },

  { path: 'course', component: CourseComponent },
  { path: 'course-details', component: CorseDetailsComponent },
  { path: 'course-check', component: CourseCheckComponent },
  { path: 'course-evaluating', component: CourseEvaluatingComponent },
  { path: 'course-grading', component: CourseGradingComponent },

  { path: 'person-manage', component: PersonManageComponent },
  { path: 'teacher-create', component: TeacherCreateComponent },
  { path: 'teacher-details', component: TeacherDetailsComponent },
  { path: 'teacher-account-create', component: TeacherAccountCreateComponent },
  { path: 'student-create', component: StudentCreateComponent },
  { path: 'student-details', component: StudentDetailsComponent },

  { path: 'subject-manage', component: SubjectManageComponent },
  { path: 'learning-area-create', component: LearningAreaCreateComponent },
  { path: 'learning-area-details', component: LearningAreaDetailsComponent },
  { path: 'subject-create', component: SubjectCreateComponent },
  { path: 'subject-details', component: SubjectDetailsComponent },

  { path: 'classroom-manage', component: ClassroomManageComponent },
  { path: 'classroom-create', component: ClassroomCreateComponent },
  { path: 'classroom-details', component: ClassroomDetailsComponent },
  { path: 'classroom-student-create', component: ClassroomStudentCreateComponent },
  { path: 'classroom-student-details', component: ClassroomStudentDetailsComponent },

  { path: 'opensubject-manage', component: OpensubjectManageComponent },
  { path: 'open-subject-create', component: OpenSubjectCreateComponent },
  { path: 'open-subject-details', component: OpenSubjectDetailsComponent },
  { path: 'register-open-subject-create', component: RegisterOpenSubjectCreateComponent },
  { path: 'register-open-subject-details', component: RegisterOpenSubjectDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
