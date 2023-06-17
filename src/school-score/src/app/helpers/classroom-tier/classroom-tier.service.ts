import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClassroomTierService {

  constructor() { }

  Get(tier: any) {
    if (tier == "PreSchool") return "อนุบาล";
    else if (tier == "PrimarySchool") return "ประถมศึกษา";
    else if (tier == "JuniorHighSchool") return "มัธยมศึกษาตอนต้น";
    else if (tier == "SeniorHighSchool") return "มัธยมศึกษาตอนปลาย";
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
