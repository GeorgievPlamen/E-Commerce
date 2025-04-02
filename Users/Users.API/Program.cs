using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Users.API.Middlewares;
using Users.Core;
using Users.Core.Mappers;
using Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddCore();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationUserMappingProfile)));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
