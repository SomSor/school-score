import { Component } from '@angular/core';

@Component({
  selector: 'app-course-check',
  templateUrl: './course-check.component.html',
  styleUrls: ['./course-check.component.css']
})
export class CourseCheckComponent {
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
