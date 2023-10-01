using InstanceEnums.PolyEnum.Extensions;
using InstanceEnums.Test.Web.ServiceContracts;
using InstanceEnums.Test.Web.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMedicationService, InsomniaMedicationService>();
builder.Services.AddTransient<IMedicationService, HypertensionMedicationService>();
builder.Services.AddTransient<IMedicationService, MedicationService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c=>{ c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo Instance Enums", Version = "v1" });});

builder.ActivateEnums();

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