import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CourseComponent } from './course/course.component';
import { CourseCheckComponent } from './course-check/course-check.component';
import { CourseGradingComponent } from './course-grading/course-grading.component';
import { CourseEvaluatingComponent } from './course-evaluating/course-evaluating.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    CourseComponent,
    CourseCheckComponent,
    CourseGradingComponent,
    CourseEvaluatingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes, { useHash: true }),
    BrowserAnimationsModule,

    MatIconModule,
    MatMenuModule,
    MatTableModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
