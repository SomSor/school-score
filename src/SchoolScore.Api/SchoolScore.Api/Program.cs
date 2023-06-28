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

builder.Services.AddTransient<IClassroomDac<Classroom>, ClassroomDac>();
builder.Services.AddTransient<IClassroomStudentDac<ClassroomStudent>, ClassroomStudentDac>();
builder.Services.AddTransient<ILearningAreaDac<LearningArea>, LearningAreaDac>();
builder.Services.AddTransient<IOpenSubjectDac<OpenSubject>, OpenSubjectDac>();
builder.Services.AddTransient<ISchoolDac<School>, SchoolDac>();
builder.Services.AddTransient<ISchoolYearDac<SchoolYear>, SchoolYearDac>();
builder.Services.AddTransient<IStudentDac<Student>, StudentDac>();
builder.Services.AddTransient<ISubjectDac<Subject>, SubjectDac>();
builder.Services.AddTransient<ITeacherDac<Teacher>, TeacherDac>();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

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
app.UseCors(config => config
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
