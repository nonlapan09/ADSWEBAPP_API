using ADSWEBAPP_API.Controllers;
using ADSWEBAPP_API.Data;
using ADSWEBAPP_API.Data.Address;
using ADSWEBAPP_API.Data.Authentication;
using ADSWEBAPP_API.Dto.AuthenData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;
using static ADSWEBAPP_API.Controllers.SwaggerControllerOrderAttribute;

//var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
//var logger = LogManager.GetLogger("databaseLogger");
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.File("Log/log-.txt", rollingInterval: RollingInterval.Hour)
//    .WriteTo.MSSqlServer(
//        connectionString: "User Id=ADSUSR;Password=adsProd65;Data Source=oracle04-scan.praisanee.com:1699/ADSDB_SRV;",
//        sinkOptions: new MSSqlServerSinkOptions { TableName = "TBL_LOGGING" })
//    .CreateLogger();

try
{

    Log.Debug("Application Starting Up");
    var builder = WebApplication.CreateBuilder(args);
    var Configuration = builder.Configuration;


    //var columnOptions = new ColumnOptions
    //{
    //    AdditionalColumns = new Collection<SqlColumn>
    //    {
    //        new SqlColumn{ ColumnName = "MESSAGE", DataType = SqlDbType.NVarChar, AllowNull = false },
    //        new SqlColumn{ ColumnName = "MESSAGETEMPLATE", DataType = SqlDbType.NVarChar, AllowNull = false },
    //        new SqlColumn { ColumnName = "LEVELS", DataType = SqlDbType.NVarChar, AllowNull = false },
    //        new SqlColumn { ColumnName = "TIMESTAMP", DataType = SqlDbType.DateTime, AllowNull = true },
    //        new SqlColumn { ColumnName = "EXCEPTION", DataType = SqlDbType.NVarChar, AllowNull = false },
    //        new SqlColumn { ColumnName = "PROPERTIES", DataType = SqlDbType.NVarChar, AllowNull = false },
    //        new SqlColumn { ColumnName = "USERNAME", DataType = SqlDbType.NVarChar, AllowNull = false }
    //    }
    //};

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext().CreateLogger();
    //.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("APIContext"), sinkOptions: new MSSqlServerSinkOptions { TableName = "WEBAPILOGS" },
    //columnOptions: columnOptions)
    //.CreateLogger();

    //string swaggerBasePath = "ads";
    //CORS
    builder.Services.AddCors();

    // ConnectOracle String 1
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseOracle(builder.Configuration.GetConnectionString("APIContext") ?? throw new InvalidOperationException("Connection string 'APIContext' not found.")));

    // ConnectOracle String 1
    builder.Services.AddDbContext<AuthenticationDbContext>(options =>
        options.UseOracle(builder.Configuration.GetConnectionString("APIContext") ?? throw new InvalidOperationException("Connection string 'APIContext' not found.")));

    //NLog: Setup NLog for Dependency injection
    
    builder.Services.AddHttpLogging(logs =>
    {
        logs.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    });

    builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = Configuration["RedisCacheUrl"]; });

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ApplicationDbContext>();
    builder.Services.AddScoped<AddressRepo>();

    builder.Services.AddScoped<AuthenticationDbContext>();
    builder.Services.AddScoped<AuthenticationRepo>();


    // For Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AuthenticationDbContext>()
        .AddDefaultTokenProviders();

    // Adding Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            //ValidateIssuer = true,
            //ValidateAudience = true,
            //ValidAudience = Configuration["JWT:ValidAudience"],
            //ValidIssuer = Configuration["JWT:ValidIssuer"],
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]!)),
            ValidateIssuerSigningKey = false,
            ValidateLifetime = true,
            ValidateIssuer = false,
            //ValidAudience = Configuration["Tokens:Audience"],
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
            JwtBearerDefaults.AuthenticationScheme);

        defaultAuthorizationPolicyBuilder =
            defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

        options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
    });


    builder.Services.AddSwaggerGen(
        option =>
        {
            option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            option.OperationFilter<SecurityRequirementsOperationFilter>();

            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ADS API",
                Description = "ADS API REST API ",
                //TermsOfService = new Uri("https://smsapp1.thailandpost.com/terms"),
                //Contact = new OpenApiContact
                //{
                //    Name = "Contact",
                //    Url = new Uri("https://smsapp1.thailandpost.com/contact")
                //},
                //License = new OpenApiLicense
                //{
                //    Name = "License",
                //    Url = new Uri("https://smsapp1.thailandpost.com/license")
                //}
            });

            SwaggerControllerOrder<ControllerBase> swaggerControllerOrder = new SwaggerControllerOrder<ControllerBase>(Assembly.GetEntryAssembly()!);
            option.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"]!)}");

        }


    );

    //builder.Logging.ClearProviders();
    //builder.Host.UseNLog();
    builder.Host.UseSerilog();
    builder.Logging.AddSerilog();

    var app = builder.Build();

    app.UseCors(options => options
            // Domain
            //.WithOrigins("https://example.com","https://example2.com")

            // Domain
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "ads/swagger/{documentName}/ads.json";
        });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/ads/swagger/v1/ads.json", "PostgreStoreAPI v1");
            c.RoutePrefix = "ads/swagger";
            c.DocumentTitle = "ADS";
            c.InjectStylesheet("/docs-ui/custom.css");
            c.InjectJavascript("/docs-ui/custom.js");
        });
    }

    app.UseHttpsRedirection();
    app.UseHttpLogging();

    app.UseSerilogRequestLogging();
    // Authentication & Authorization
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Stopped program because of exception");
    throw;
}
finally
{
    //NLog.LogManager.Shutdown();
    Log.CloseAndFlush();
}


//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using NLog.Web;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ADSWEBAPP_API
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            //CreateHostBuilder(args).Build().Run();
//            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
//            try
//            {
//                logger.Debug("Application Starting Up");
//                CreateHostBuilder(args).Build().Run();
//            }
//            catch (Exception exception)
//            {
//                logger.Error(exception, "Stopped program because of exception");
//                throw;
//            }
//            finally
//            {
//                NLog.LogManager.Shutdown();
//            }
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                })
//            .ConfigureLogging(logging =>
//            {
//                logging.ClearProviders();
//                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
//            })
//            .UseNLog();
//    }
//}
