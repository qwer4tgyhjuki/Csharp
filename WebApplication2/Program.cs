using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication2.Services.AnimeService;
using WebApplication2.Services.ApiService.ApiService;
using WebApplication2.Services.AuthService;
using WebApplication2.Services.MangakaService;
using WebApplication2.Services.UserService;
using WebApplication2.V2.Controllers;
using WebApplication2.V1.Services;
using WebApplication2.V2.Services;
using WebApplication2.V3.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication2;
using WebApplication2.Services.HealthChecker;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using WebApplication2.Services.DbService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<ApplicationDbContext>(options => options
 .UseNpgsql(
         builder.Configuration.GetConnectionString("DefaultConnection")
         ));
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization (Bearer scheme)",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(optins =>
{
    optins.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    optins.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.Conventions.Controller<TestVersionController>();
    options.Conventions.Add(new VersionByNamespaceConvention());
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors();
builder.Services.AddHealthChecks()
    .AddTypeActivatedCheck<AnimeHealthCheck>("anime_health_check", args: new object[] { "Token type - JWT Bearer" })
    .AddDbContextCheck<ApplicationDbContext>("database_health_check");


builder.Services.AddHealthChecksUI(o =>
{
    o.AddHealthCheckEndpoint("anime_health_check", "/anime_health");
    o.AddHealthCheckEndpoint("database_health_check", "/database_health");
}).AddInMemoryStorage();


builder.Services.AddTransient<ITestVersionServiceV1, TestVersionServiceV1>();
builder.Services.AddTransient<ITestVersionServiceV2, TestVersionServiceV2>();
builder.Services.AddTransient<ITestVersionServiceV3, TestVersionServiceV3>();
builder.Services.AddTransient<IApiService, ApiService>(); 
builder.Services.AddSingleton<IAnimeService, AnimeService>(); // It is used both for initializing objects, but also in UserModel.
builder.Services.AddSingleton<IUserService, UserService>(); // Means that every client request will create a new instance and isolate state between different clients
builder.Services.AddTransient<IMangakaService, MangakaService>(); // Same like UserService
builder.Services.AddSingleton<IAuthService, AuthService>();


//builder.Services.AddSingleton<IHealthCheck, AnimeHealthCheck>();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            c.SwaggerEndpoint(url, name);
        }
    });
}
app.UseRouting();

app.MapHealthChecks("/anime_health",
    new HealthCheckOptions
    {
        Predicate = reg => reg.Name == "anime_health_check",
        AllowCachingResponses = false,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.MapHealthChecks("/database_health",
    new HealthCheckOptions
    {
        Predicate = reg => reg.Name == "database_health_check",
        AllowCachingResponses = false,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();


app.UseHealthChecksUI(options =>
{
    options.UIPath = "/health";
});

app.Run();
