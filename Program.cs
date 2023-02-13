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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongo().AddMongoRepository<Transactions>("fourth");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
