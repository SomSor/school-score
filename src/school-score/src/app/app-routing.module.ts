import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConfigurationComponent } from './configuration/configuration.component';
import { CourseComponent } from './course/course.component';
import { CourseCheckComponent } from './course-check/course-check.component';
import { CourseEvaluatingComponent } from './course-evaluating/course-evaluating.component';
import { CourseGradingComponent } from './course-grading/course-grading.component';
import { DataManagementComponent } from './data-management/data-management.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ReportComponent } from './report/report.component';

import { CourseEvaluatingTempComponent } from './course-evaluating-temp/course-evaluating-temp.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'configuration', component: ConfigurationComponent },
  { path: 'course', component: CourseComponent },
  { path: 'course-check', component: CourseCheckComponent },
  { path: 'course-evaluating', component: CourseEvaluatingComponent },
  { path: 'course-grading', component: CourseGradingComponent },
  { path: 'data-management', component: DataManagementComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'report', component: ReportComponent },

  { path: 'course-evaluating-temp', component: CourseEvaluatingTempComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
