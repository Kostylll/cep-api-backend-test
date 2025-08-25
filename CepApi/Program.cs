using CepApi.Application.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using CepApi.Extension;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFeatureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

builder.Services.AddDbContext<CepApiDbContext>(options =>
{
    var cnn = builder.Configuration.GetConnectionString("ViaCepDb");
    options.UseMySql(cnn, ServerVersion.AutoDetect(cnn));
});

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ViaCep API",
        Version = "V1",
        Description = "This Api is for consulting the informations about your CEP(Codigo de Endereçamento Postal) and you can see all the CEP you consulted!",
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
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
