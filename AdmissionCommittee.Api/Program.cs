using AdmissionCommittee.Api.Middleware;
using AdmissionCommittee.Domain;
using AdmissionCommittee.Domain.Attributes;
using AdmissionCommittee.Domain.Repositories;
using AdmissionCommittee.Domain.Services;

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
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IAbiturientService, AbiturientService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<IExamResultService, ExamResultService>();
builder.Services.AddSingleton<IAbiturientRepository, InMemoryAbiturientRepository>();
builder.Services.AddSingleton<IApplicationRepository, InMemoryApplicationRepository>();
builder.Services.AddSingleton<ISpecialityRepository, InMemorySpecialityRepository>();
builder.Services.AddSingleton<IExamResultRepository, InMemoryExamResultRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


var app = builder.Build();

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
