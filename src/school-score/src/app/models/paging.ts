export class PagingModel {
    Data: any;
    Length = 0;
    PageIndex = 0;
    PageSize = 50;
    LastCount = 0;
    SearchText = "";
}

export class RegisteredOpenSubject extends PagingModel{
    Subjects: any;
    SubjectIds: any;
    OpenSubjects: any;
}

export class SchoolYearPaging extends PagingModel{
    Current: any;
}