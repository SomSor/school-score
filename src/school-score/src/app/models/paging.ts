import { PageEvent } from "@angular/material/paginator";

export class PagingModel {
    Data: any;
    Length = 0;
    PageIndex = 0;
    PageSize = 50;
    LastCount = 0;
    SearchText = "";
}