import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConfigurationComponent } from './configuration/configuration.component';
import { CourseComponent } from './course/course.component';
import { CourseCheckComponent } from './course-check/course-check.component';
import { CourseEvaluatingComponent } from './course-evaluating/course-evaluating.component';
import { CourseGradingComponent } from './course-grading/course-grading.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ReportComponent } from './report/report.component';

import { DataManagementComponent } from './data-management/data-management/data-management.component';
import { LearningAreaCreateComponent } from './data-management/learning-area/learning-area-create/learning-area-create.component';
import { LearningAreaDetailsComponent } from './data-management/learning-area/learning-area-details/learning-area-details.component';
import { SubjectCreateComponent } from './data-management/subject/subject-create/subject-create.component';
import { SubjectDetailsComponent } from './data-management/subject/subject-details/subject-details.component';
import { TeacherCreateComponent } from './data-management/teacher/teacher-create/teacher-create.component';
import { TeacherDetailsComponent } from './data-management/teacher/teacher-details/teacher-details.component';
import { StudentCreateComponent } from './data-management/student/student-create/student-create.component';
import { StudentDetailsComponent } from './data-management/student/student-details/student-details.component';

import { CourseEvaluatingTempComponent } from './course-evaluating-temp/course-evaluating-temp.component';
import { ClassroomCreateComponent } from './data-management/classroom/classroom-create/classroom-create.component';
import { ClassroomDetailsComponent } from './data-management/classroom/classroom-details/classroom-details.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'configuration', component: ConfigurationComponent },
  { path: 'course', component: CourseComponent },
  { path: 'course-check', component: CourseCheckComponent },
  { path: 'course-evaluating', component: CourseEvaluatingComponent },
  { path: 'course-grading', component: CourseGradingComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'report', component: ReportComponent },
  
  { path: 'data-management', component: DataManagementComponent },
  { path: 'learning-area-create', component: LearningAreaCreateComponent },
  { path: 'learning-area-details', component: LearningAreaDetailsComponent },
  { path: 'subject-create', component: SubjectCreateComponent },
  { path: 'subject-details', component: SubjectDetailsComponent },
  { path: 'teacher-create', component: TeacherCreateComponent },
  { path: 'teacher-details', component: TeacherDetailsComponent },
  { path: 'student-create', component: StudentCreateComponent },
  { path: 'student-details', component: StudentDetailsComponent },
  { path: 'classroom-create', component: ClassroomCreateComponent },
  { path: 'classroom-details', component: ClassroomDetailsComponent },

  { path: 'course-evaluating-temp', component: CourseEvaluatingTempComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
