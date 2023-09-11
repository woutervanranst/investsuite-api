using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserApi.Database;

namespace UserApi;

public class Program
{
    public static void Main(string[] args)
    {
        // Tutorial: https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization

        var builder = WebApplication.CreateBuilder(args);

        // Configure logging services.
        ConfigureLogging(builder);

        // Register application services.
        ConfigureServices(builder);

        // Build the application.
        var app = builder.Build();

        // Set up the middleware pipeline.
        ConfigureAppPipeline(app);

        // Start and run the web application.
        app.Run();
    }

    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        AddCosmosDbServices(builder);

        AddSwaggerServices(builder);

        AddAuth0(builder);

        builder.Services.AddControllers();
        
        static void AddCosmosDbServices(WebApplicationBuilder builder)
        {
            var cosmosEndpoint     = builder.Configuration["CosmosDb:Endpoint"];
            var cosmosKey          = builder.Configuration["CosmosDb:Key"];
            var cosmosDatabaseName = builder.Configuration["CosmosDb:DatabaseName"];
            var containerName      = builder.Configuration["CosmosDb:ContainerName"];

            // Register the CosmosDB context service with the required configuration.
            builder.Services.AddScoped<UserCosmosDbContext>(_ =>
                new UserCosmosDbContext(cosmosEndpoint, cosmosKey, cosmosDatabaseName, containerName));
        }

        static void AddSwaggerServices(WebApplicationBuilder builder)
        {
            // See configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();

            // Register Swagger generator service.
            builder.Services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserApi", Version = "v1" });

                // Define JWT Bearer authentication for Swagger.
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In          = ParameterLocation.Header,
                    Type        = SecuritySchemeType.Http,
                    Scheme      = "bearer"
                });

                // Add a security requirement for Bearer tokens.
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        static void AddAuth0(WebApplicationBuilder builder)
        {
            var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

            // Register JWT Bearer authentication services.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience  = builder.Configuration["Auth0:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            // Define authorization policies.
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("read:user", policy => policy.Requirements.Add(new HasScopeRequirement("read:user", domain)));
            });

            // Register custom authorization handler.
            builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }

    /// <summary>
    /// Configure the middleware pipeline for the application.
    /// </summary>
    private static void ConfigureAppPipeline(WebApplication app)
    {
        // Use Swagger in development environment.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Use HTTPS redirection middleware.
        app.UseHttpsRedirection();

        // Use endpoint routing.
        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
    }
}