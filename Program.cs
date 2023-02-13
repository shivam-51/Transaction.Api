using Transaction.Api.Repositories;
using Transaction.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<MongoDbSettings>( builder.Configuration.GetSection("MongoDb"));

builder.Services.AddControllers(
    options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
    }
).AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongo().AddMongoRepository<Transactions>("anotherCollection");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
