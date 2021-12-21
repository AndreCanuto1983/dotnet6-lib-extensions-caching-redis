using WorkingWithRedis.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Redis.Configurations(builder);
DependencyInjection.Configurations(builder.Services);
ServiceExtension.Configurations(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomExceptionMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
