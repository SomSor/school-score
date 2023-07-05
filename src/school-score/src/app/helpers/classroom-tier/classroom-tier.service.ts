import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClassroomTierService {

  constructor() { }

  Get(tier: any) {
    if (tier == "PreSchool") return "อนุบาล";
    else if (tier == "PrimarySchool") return "ประถมศึกษาปีที่";
    else if (tier == "JuniorHighSchool") return "มัธยมศึกษาปีที่";
    else if (tier == "SeniorHighSchool") return "มัธยมศึกษาปีที่";
    else return "";
  }

  GetAbb(tier: any) {
    if (tier == "PreSchool") return "อ.";
    else if (tier == "PrimarySchool") return "ป.";
    else if (tier == "JuniorHighSchool") return "ม.";
    else if (tier == "SeniorHighSchool") return "ม.";
    else return "";
  }
}
