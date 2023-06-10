using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SchoolScore.Api.DACs;
using SchoolScore.Api.DACs.Imps;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Helpers;
using SchoolScore.Api.Models.Configuration;
using System.Text;

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
builder.Services.AddTransient<ISujectDac<Subject>, SujectDac>();
builder.Services.AddTransient<ITeacherDac<Teacher>, TeacherDac>();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
        };
    });
builder.Services.AddScoped<JwtHandler>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
