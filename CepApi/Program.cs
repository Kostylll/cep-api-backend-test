using CepApi.Application.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using ViaCepApi.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFeatureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CepApiDbContext>(options =>
{
    var cnn = builder.Configuration.GetConnectionString("CepApiDb");
    options.UseMySql(cnn, ServerVersion.AutoDetect(cnn));
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
