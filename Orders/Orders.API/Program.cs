
using FluentValidation.AspNetCore;
using Orders.BLL;
using Orders.BLL.HttpClients;
using Orders.BLL.PollyPolicies;
using Orders.DAL;
using Users.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDAL(builder.Configuration);
builder.Services.AddBLL(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
}));

builder.Services.AddScoped<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddScoped<IProductsMicroservicePolicies, ProductsMicroservicePolicies>();

builder.Services.AddHttpClient<UsersMicroserviceClient>(client =>
{
    var usersMicroserviceName = builder.Configuration["UsersMicroserviceName"];
    var usersMicroservicePort = builder.Configuration["UsersMicroservicePort"];

    client.BaseAddress = new Uri($"http://{usersMicroserviceName}:{usersMicroservicePort}");
}).AddPolicyHandler((services, _) => services.GetRequiredService<IUsersMicroservicePolicies>().GetCombinedPolicy());

builder.Services.AddHttpClient<ProductsMicroserviceClient>(client =>
{
    var productsMicroserviceName = builder.Configuration["ProductsMicroserviceName"];
    var productsMicroservicePort = builder.Configuration["ProductsMicroservicePort"];

    client.BaseAddress = new Uri($"http://{productsMicroserviceName}:{productsMicroservicePort}");
}).AddPolicyHandler((services, _) => services.GetRequiredService<IProductsMicroservicePolicies>().GetFallbackPolicy())
    .AddPolicyHandler((services, _) => services.GetRequiredService<IProductsMicroservicePolicies>().GetBulkheadIsolationPolicy());


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
