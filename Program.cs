using Transaction.Api.Repositories;
using Transaction.Api.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDbSettings>( builder.Configuration.GetSection("MongoDb"));

builder.Services.AddControllers(
    options => options.SuppressAsyncSuffixInActionNames = false)
    .AddJsonOptions(
        options => {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

builder.Services.AddControllers().AddDapr();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongo().AddMongoRepository<Transactions>("fourth");

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseHttpsRedirection();
app.UseAuthorization();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("event-processor");

app.MapControllers();
app.MapGet("/health", async (HttpContext context) =>{
     logger.LogInformation("Running health check."); 
     await context.Response.WriteAsync("Running");}
);
app.Run();
