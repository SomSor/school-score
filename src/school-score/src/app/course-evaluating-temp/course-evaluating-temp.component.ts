import { Component } from '@angular/core';

@Component({
  selector: 'app-course-evaluating-temp',
  templateUrl: './course-evaluating-temp.component.html',
  styleUrls: ['./course-evaluating-temp.component.css']
})
export class CourseEvaluatingTempComponent {
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
