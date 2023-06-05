using SchoolScore.Api.DACs;
using SchoolScore.Api.DACs.Imps;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient(x => builder.Configuration.GetSection(nameof(MongoDBConfiguration)).Get<MongoDBConfiguration>());

builder.Services.AddTransient<IClassRoomDac<ClassRoom>, ClassRoomDac>();
builder.Services.AddTransient<IClassRoomStudentDac<ClassRoomStudent>, ClassRoomStudentDac>();
builder.Services.AddTransient<IGradingCriteriaDac<GradingCriteria>, GradingCriteriaDac>();
builder.Services.AddTransient<ILearningAreaDac<LearningArea>, LearningAreaDac>();
builder.Services.AddTransient<IOpenSubjectDac<OpenSubject>, OpenSubjectDac>();
builder.Services.AddTransient<ISchoolDac<School>, SchoolDac>();
builder.Services.AddTransient<ISchoolYearDac<SchoolYear>, SchoolYearDac>();
builder.Services.AddTransient<IScoringGroupDac<ScoringGroup>, ScoringGroupDac>();
builder.Services.AddTransient<IStudentDac<Student>, StudentDac>();
builder.Services.AddTransient<IStudentRegisterOpenSubjectDac<StudentRegisterOpenSubject>, StudentRegisterOpenSubjectDac>();
builder.Services.AddTransient<ISujectDac<Suject>, SujectDac>();
builder.Services.AddTransient<ITeacherDac<Teacher>, TeacherDac>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
