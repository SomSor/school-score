import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
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

import { SkeletonComponent } from './helpers/skeleton/skeleton.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ReportComponent } from './report/report.component';
import { ConfigurationComponent } from './configuration/configuration.component';

import { CourseComponent } from './course/course.component';
import { CourseCheckComponent } from './course-check/course-check.component';
import { CourseGradingComponent } from './course-grading/course-grading.component';
import { CourseEvaluatingComponent } from './course-evaluating/course-evaluating.component';

import { PersonManageComponent } from './data-management/data-management/person-manage/person-manage.component';
import { TeacherCreateComponent } from './data-management/teacher/teacher-create/teacher-create.component';
import { TeacherDetailsComponent } from './data-management/teacher/teacher-details/teacher-details.component';
import { StudentCreateComponent } from './data-management/student/student-create/student-create.component';
import { StudentDetailsComponent } from './data-management/student/student-details/student-details.component';

import { SubjectManageComponent } from './data-management/data-management/subject-manage/subject-manage.component';
import { LearningAreaCreateComponent } from './data-management/learning-area/learning-area-create/learning-area-create.component';
import { LearningAreaDetailsComponent } from './data-management/learning-area/learning-area-details/learning-area-details.component';
import { SubjectCreateComponent } from './data-management/subject/subject-create/subject-create.component';
import { SubjectDetailsComponent } from './data-management/subject/subject-details/subject-details.component';

import { ClassroomManageComponent } from './data-management/data-management/classroom-manage/classroom-manage.component';
import { ClassroomDetailsComponent } from './data-management/classroom/classroom-details/classroom-details.component';
import { ClassroomCreateComponent } from './data-management/classroom/classroom-create/classroom-create.component';
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

const routes: Routes = [];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    ReportComponent,
    ConfigurationComponent,

    CourseComponent,
    CourseCheckComponent,
    CourseGradingComponent,
    CourseEvaluatingComponent,

    PersonManageComponent,
    TeacherCreateComponent,
    TeacherDetailsComponent,
    StudentCreateComponent,
    StudentDetailsComponent,

    SubjectManageComponent,
    LearningAreaCreateComponent,
    LearningAreaDetailsComponent,
    SkeletonComponent,
    SubjectCreateComponent,
    SubjectDetailsComponent,

    ClassroomManageComponent,
    ClassroomDetailsComponent,
    ClassroomCreateComponent,
    ClassroomStudentCreateComponent,
    ClassroomStudentDetailsComponent,

    OpensubjectManageComponent,
    OpenSubjectCreateComponent,
    OpenSubjectDetailsComponent,
    RegisterOpenSubjectCreateComponent,
    RegisterOpenSubjectDetailsComponent,
    CorseDetailsComponent,
    SchoolYearCreateComponent,
    SchoolYearDetailsComponent,
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
    MatCheckboxModule,
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
