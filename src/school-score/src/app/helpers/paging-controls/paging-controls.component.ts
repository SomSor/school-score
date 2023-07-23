import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { PagingModel } from 'src/app/models/paging';

@Component({
  selector: 'app-paging-controls',
  templateUrl: './paging-controls.component.html',
  styleUrls: ['./paging-controls.component.css']
})
export class PagingControlsComponent {

  @Input() data = new PagingModel;
  @Input() addButtonText: any;
  @Input() link: any;
  @Input() showSearch: any;
  @Output() getDataFunc = new EventEmitter<any>();

  constructor() {
  }

  async getData(event?: PageEvent) {
    console.log(this.getDataFunc);

    await this.getDataFunc.emit(event);
    return event;
  }
}
