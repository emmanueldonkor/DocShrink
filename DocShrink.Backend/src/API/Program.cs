using API;
using API.Extensions;
using Application;
using AspNetCoreRateLimit;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.RegisterCors();

builder.Services.AddControllers();

builder.Services.AddApiVersioning();


builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DocShrinkPolicy");

app.UseIpRateLimiting();

app.MapControllers();

app.Run();
