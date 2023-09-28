using InstanceEnums;
using InstanceEnums.PolyEnum.Extensions;
using InstanceEnums.PolyEnum.ModelBinding.ModelBinderProviders;
using InstanceEnums.PolyEnum.Swagger;
using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.ServiceContracts;
using InstanceEnums.Test.Web.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AgeGroups>();
builder.Services.AddSingleton<DiagnosisTypes>();

builder.Services.AddTransient<IMedicationService, InsomniaMedicationService>();
builder.Services.AddTransient<IMedicationService, HypertensionMedicationService>();
builder.Services.AddTransient<IMedicationService, MedicationService>();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new EnumModelBinderProvider());
});

builder.Services.AddEndpointsApiExplorer();

builder.ActivateEnums();

builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Document Conversion API", Version = "v1" });
    c.AddInstanceEnums();
});


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