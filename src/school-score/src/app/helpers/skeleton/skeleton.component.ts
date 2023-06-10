import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-skeleton',
  templateUrl: './skeleton.component.html',
  styleUrls: ['./skeleton.component.css']
})
export class SkeletonComponent {

  @Input()
  style: any;

  @Input()
  row: any;

  @Input()
  column: any;

  data: any = [];

  ngOnInit() {
    this.style ??= "detail";
    this.row ??= 4;
    
    this.column ??= 1;

    for (var i = 0; i < this.row; i++) {
      let rowData = []
      for (var j = 0; j < this.column; j++) {
        rowData.push("Col" + (j + 1));
      }
      this.data.push(rowData);
    }
  }
}
