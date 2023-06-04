import { Component } from '@angular/core';

@Component({
  selector: 'app-course-evaluating',
  templateUrl: './course-evaluating.component.html',
  styleUrls: ['./course-evaluating.component.css']
})
export class CourseEvaluatingComponent {
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
