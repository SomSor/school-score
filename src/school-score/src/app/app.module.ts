import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CourseComponent } from './course/course.component';
import { CourseCheckComponent } from './course-check/course-check.component';
import { CourseGradingComponent } from './course-grading/course-grading.component';
import { CourseEvaluatingComponent } from './course-evaluating/course-evaluating.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { CourseEvaluatingTempComponent } from './course-evaluating-temp/course-evaluating-temp.component';
import { DataManagementComponent } from './data-management/data-management.component';
import { RegistrationComponent } from './registration/registration.component';
import { ConfigurationComponent } from './configuration/configuration.component';
import { ReportComponent } from './report/report.component';

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
    DataManagementComponent,
    RegistrationComponent,
    ConfigurationComponent,
    ReportComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes, { useHash: true }),
    BrowserAnimationsModule,

    FlexLayoutModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatMenuModule,
    MatTableModule,
    MatTabsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
