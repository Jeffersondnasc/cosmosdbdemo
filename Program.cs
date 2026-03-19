using Microsoft.Azure.Cosmos;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Criação automática do container Pessoa

var cosmosConfig = builder.Configuration.GetSection("CosmosDb");
var cosmosClient = new CosmosClient(
    cosmosConfig["Account"],
    cosmosConfig["Key"]);
var databaseId = cosmosConfig["DatabaseName"];
var containerId = "Pessoa";
var partitionKey = "/id";

// Garante que o banco e o container existam
var database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).GetAwaiter().GetResult();
database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties(containerId, partitionKey)).GetAwaiter().GetResult();

builder.Services.AddSingleton(s => cosmosClient);

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CosmosDB API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CosmosDB API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
