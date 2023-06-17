import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
// import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ConfigurationComponent } from './configuration/configuration.component';
import { CourseComponent } from './course/course.component';
import { CourseCheckComponent } from './course-check/course-check.component';
import { CourseGradingComponent } from './course-grading/course-grading.component';
import { CourseEvaluatingComponent } from './course-evaluating/course-evaluating.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { CourseEvaluatingTempComponent } from './course-evaluating-temp/course-evaluating-temp.component';
import { RegistrationComponent } from './registration/registration.component';
import { ReportComponent } from './report/report.component';

import { DataManagementComponent } from './data-management/data-management/data-management.component';
import { LearningAreaCreateComponent } from './data-management/learning-area/learning-area-create/learning-area-create.component';
import { LearningAreaDetailsComponent } from './data-management/learning-area/learning-area-details/learning-area-details.component';
import { SubjectCreateComponent } from './data-management/subject/subject-create/subject-create.component';
import { SubjectDetailsComponent } from './data-management/subject/subject-details/subject-details.component';

import { SkeletonComponent } from './helpers/skeleton/skeleton.component';
import { TeacherCreateComponent } from './data-management/teacher/teacher-create/teacher-create.component';
import { TeacherDetailsComponent } from './data-management/teacher/teacher-details/teacher-details.component';
import { StudentCreateComponent } from './data-management/student/student-create/student-create.component';
import { StudentDetailsComponent } from './data-management/student/student-details/student-details.component';
import { ClassroomDetailsComponent } from './data-management/classroom/classroom-details/classroom-details.component';
import { ClassroomCreateComponent } from './data-management/classroom/classroom-create/classroom-create.component';

const routes: Routes = [];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    CourseComponent,
    CourseCheckComponent,
    CourseGradingComponent,
    CourseEvaluatingComponent,
    CourseEvaluatingTempComponent,
    RegistrationComponent,
    ConfigurationComponent,
    ReportComponent,

    DataManagementComponent,
    LearningAreaCreateComponent,
    LearningAreaDetailsComponent,
    SkeletonComponent,
    SubjectCreateComponent,
    SubjectDetailsComponent,
    TeacherCreateComponent,
    TeacherDetailsComponent,
    StudentCreateComponent,
    StudentDetailsComponent,
    ClassroomDetailsComponent,
    ClassroomCreateComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes, { useHash: true }),

    FlexLayoutModule,
    MatButtonModule,
    MatCardModule,
    MatDialogModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    // MatPaginatorModule,
    MatSelectModule,
    MatSnackBarModule,
    MatTableModule,
    MatTabsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
