using InstanceEnums.PolyEnum.ModelBinding.ModelBinderProviders;
using InstanceEnums.Test.Web.Managers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDiagnosisManager, HighBloodPresureManager>();
builder.Services.AddScoped<IDiagnosisManager, SleepingTooLittleManager>();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new EnumModelBinderProvider());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{ c.MapType<IDiagnosisManager>(() => new OpenApiSchema { Type = "string", Format = "date" }); });

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
