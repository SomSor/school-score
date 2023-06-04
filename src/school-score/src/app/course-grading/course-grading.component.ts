import { Component } from '@angular/core';

@Component({
  selector: 'app-course-grading',
  templateUrl: './course-grading.component.html',
  styleUrls: ['./course-grading.component.css']
})
export class CourseGradingComponent {
  data: any = {
    Data: [
      {
        ClassRoom: {
          ClassYear: 3,
          SubClass: 2,
        },
        Subject: {
          Code: "001512",
          Name: "คณิตศาสตร์",
        },
      },
      {
        ClassRoom: {
          ClassYear: 3,
          SubClass: 2,
        },
        Subject: {
          Code: "001512",
          Name: "คณิตศาสตร์",
        },
      },
      {
        ClassRoom: {
          ClassYear: 3,
          SubClass: 2,
        },
        Subject: {
          Code: "001512",
          Name: "คณิตศาสตร์",
        },
      },
      {
        ClassRoom: {
          ClassYear: 3,
          SubClass: 2,
        },
        Subject: {
          Code: "001512",
          Name: "คณิตศาสตร์",
        },
      },
    ],
    LastCount: 1,
  };
}
