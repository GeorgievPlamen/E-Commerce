using FluentValidation.AspNetCore;
using Products.API.Middlewares;
using Products.BLL;
using Products.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBLL();
builder.Services.AddDAL(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
