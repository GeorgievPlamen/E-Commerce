
using FluentValidation.AspNetCore;
using Orders.BLL;
using Orders.BLL.HttpClients;
using Orders.DAL;
using Users.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDAL(builder.Configuration);
builder.Services.AddBLL();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
}));

builder.Services.AddHttpClient<UsersMicroserviceClient>(client =>
{
    var usersMicroserviceName = builder.Configuration["UsersMicroserviceName"];
    var usersMicroservicePort = builder.Configuration["UsersMicroservicePort"];

    client.BaseAddress = new Uri($"http://{usersMicroserviceName}:{usersMicroservicePort}");
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.Run();
