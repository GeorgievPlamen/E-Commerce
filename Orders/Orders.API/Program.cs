
using FluentValidation.AspNetCore;
using Orders.BLL;
using Orders.BLL.HttpClients;
using Orders.DAL;
using Polly;
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
}).AddPolicyHandler(
    Policy
        .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(
            retryCount: 5,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(2),
            onRetry: (outcome, timespan, retryAttempt, context) =>
            {
                // TODO : add logs
            })
);

builder.Services.AddHttpClient<ProductsMicroserviceClient>(client =>
{
    var productsMicroserviceName = builder.Configuration["ProductsMicroserviceName"];
    var productsMicroservicePort = builder.Configuration["ProductsMicroservicePort"];

    client.BaseAddress = new Uri($"http://{productsMicroserviceName}:{productsMicroservicePort}");
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
