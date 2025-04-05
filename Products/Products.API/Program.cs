using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Products.API.ApiEndpoints;
using Products.API.Middlewares;
using Products.BLL;
using Products.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBLL();
builder.Services.AddDAL(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.ConfigureHttpJsonOptions(opt =>
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
}));

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapProducts();

app.MapGet("api/health", () => Results.Ok("Hi, from Products Service"));

app.Run();
