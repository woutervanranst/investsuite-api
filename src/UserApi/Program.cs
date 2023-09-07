
using UserApi.Database;

namespace UserApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var cosmosEndpoint     = builder.Configuration["CosmosDb:Endpoint"];
            var cosmosKey          = builder.Configuration["CosmosDb:Key"];
            var cosmosDatabaseName = builder.Configuration["CosmosDb:DatabaseName"];
            var containerName      = builder.Configuration["CosmosDb:ContainerName"];

            // Add services to the container.
            builder.Services.AddScoped<UserCosmosDbContext>(_ => new UserCosmosDbContext(cosmosEndpoint, cosmosKey, cosmosDatabaseName, containerName));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}