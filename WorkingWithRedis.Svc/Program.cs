using WorkingWithRedis.Configurations;
using WorkingWithRedis.Svc.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Redis();
builder.Services.DependencyInjection();
builder.Services.ConfigureJson();
builder.Services.ControllerCache();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapHealthChecks("/healthcheck");

if (app.Environment.IsDevelopment())
{
    app.UseCustomExceptionMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCaching(); //using controller cache => ref line 10
app.UseAuthorization();
app.MapControllers();
app.Run();
