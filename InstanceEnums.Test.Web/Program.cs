using InstanceEnums;
using InstanceEnums.PolyEnum.ModelBinding;
using InstanceEnums.PolyEnum.ModelBinding.ModelBinderProviders;
using InstanceEnums.PolyEnum.Swagger;
using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.Managers;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Text.Json.Serialization;

EnumRegistry.RegisterEnum<DiagnosisTypes, DiagnosisTypes.IDiagnosisType>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterEnumServiceScoped<IDiagnosisManager, HighBloodPresureManager>();
builder.Services.RegisterEnumServiceScoped<IDiagnosisManager, SleepingTooLittleManager>();
builder.Services.RegisterEnumServiceScoped<IDiagnosisManager, DiagnosisManager>();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new EnumModelBinderProvider());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Document Conversion API", Version = "v1" });
    c.AddInstanceEnums();
});

var app = builder.Build();

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
