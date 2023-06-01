import { Component } from '@angular/core';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent {
  data = {
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
