using AdmissionCommittee.Api.Middleware;
using AdmissionCommittee.Domain;
using AdmissionCommittee.Domain.Attributes;
using AdmissionCommittee.Domain.Data;
using AdmissionCommittee.Domain.Repositories;
using AdmissionCommittee.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IAbiturientService, AbiturientService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<IExamResultService, ExamResultService>();
builder.Services.AddScoped<IAbiturientRepository, AbiturientRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<IExamResultRepository, ExamResultRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23))));

var app = builder.Build();
app.UseCors();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
