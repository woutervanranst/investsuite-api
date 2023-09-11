using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using UserApi.Database;

namespace UserApi;

public class Program
{
    public static void Main(string[] args)
    {
        // Tutorial: https://auth0.com/docs/quickstart/backend/aspnet-core-webapi/01-authorization

        var builder = WebApplication.CreateBuilder(args);


        // Add logging
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);


        // Add services to the container.
        var cosmosEndpoint     = builder.Configuration["CosmosDb:Endpoint"];
        var cosmosKey          = builder.Configuration["CosmosDb:Key"];
        var cosmosDatabaseName = builder.Configuration["CosmosDb:DatabaseName"];
        var containerName      = builder.Configuration["CosmosDb:ContainerName"];
        builder.Services.AddScoped<UserCosmosDbContext>(_ => new UserCosmosDbContext(cosmosEndpoint, cosmosKey, cosmosDatabaseName, containerName));
        

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            //c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserApi", Version = "v1" });

            //// Define the Bearer scheme that uses JWT as the security scheme for the API.
            //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //{
            //    Description = "JWT Authorization header using the Bearer scheme.",
            //    Name        = "Authorization",
            //    In          = ParameterLocation.Header,
            //    Type        = SecuritySchemeType.Http,
            //    Scheme      = "bearer"
            //});

            //c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id   = "Bearer"
            //            }
            //        },
            //        new string[] {}
            //    }
            //});
        });


        // Add authentication and authorization
        var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
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
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("read:user", policy => policy.Requirements.Add(new
                HasScopeRequirement("read:user", domain)));
        });
        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        builder.Services.AddControllers();

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        //app.Use(async (context, next) =>
        //{
        //    var identity = context.User.Identity;
        //    if (identity != null && identity.IsAuthenticated)
        //    {
        //        //_logger.LogInformation($"User {identity.Name} is authenticated with {identity.AuthenticationType}.");
        //    }
        //    else
        //    {
        //        //_logger.LogInformation("User is not authenticated.");
        //    }

        //    await next.Invoke();
        //});


        app.Run();
    }
}