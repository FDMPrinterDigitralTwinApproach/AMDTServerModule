using AMDTServerModule.Helpers;
using AMDTServerModule.MiddleWares;
using AMDTServerModule.Models;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using System.IO.Compression;
using System.Text;

using static MyBackgroundService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy("AllowAnyOrigin",
builder =>
{
    builder.AllowAnyOrigin()
   .AllowAnyMethod()
    .AllowAnyHeader();
}));
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.StackBlockedRequests = true;
    options.HttpStatusCode = 429;
    options.EnableEndpointRateLimiting = true;
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*",
            Limit = 50, // 10 saniyede 50 istek
            Period = "1s"
        }
    };
});
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddMemoryCache();

builder.Services.AddMetrics(); // for measure metrics

//builder.Services.AddScoped<MetricReporter>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AutomaticAuthentication = false;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Barida Portal API",  Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddDbContextPool<AmDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
});
builder.Services.AddHostedService<MyBackgroundService>();
builder.Services.AddSingleton<IAuthorizationHandler, RolesAuthorizationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpMetrics(); // for measure metrics default
//app.UseMiddleware<MetricMiddleWare>();
app.UseMetricServer();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapMetrics();

app.MapControllers();


app.Run("http://*:5100/");
public class MyBackgroundService : BackgroundService
{
   
    public MyBackgroundService()
    {

    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var task2 = Task.Run(() => GatheringData(stoppingToken), stoppingToken);

        await Task.WhenAll( task2);
    }

    private async Task GatheringData(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("ALo");
            Thread.Sleep(100000);
        }

    }

    
}