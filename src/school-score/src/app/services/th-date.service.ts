import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThDateService {
  
  dayOfWeeks: any = ["อา.", "จ.", "อ.", "พ.", "พฤ.", "ศ.", "ส."];
  monthOfYears: any = ["ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค."];

  constructor() { }

  THDate(dateStr: any): any {
    let date = new Date(dateStr);
    let thDate = `${date.getDate()} ${this.monthOfYears[date.getMonth()]} ${date.getFullYear() + 543}`;
    return thDate;
  }

}
