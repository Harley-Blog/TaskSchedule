using BC.TS.Api.Filters;
using BC.TS.Application.Service.Facade;
using BC.TS.Application.Service.Implement;
using BC.TS.Domain.Dispatcher.Repository.Facade;
using BC.TS.Domain.Dispatcher.Service.Facade;
using BC.TS.Domain.Dispatcher.Service.Implement;
using BC.TS.Repository;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add health check
builder.Services.AddHealthChecks();
    //.AddSqlServer(builder.Configuration.GetConnectionString("MSConnection"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        //jwt  token
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            ValidateLifetime = false,
            ValidAudience = "BC.TS.API",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecurityKey")),
            ClockSkew = TimeSpan.Zero
        };
    });

// Add AutoMapper
builder.Services.AddAutoMapper(
    Assembly.Load("BC.TS.Application"),
    Assembly.Load("BC.TS.Domain")
    );

// Add MediatR
builder.Services.AddMediatR(
    Assembly.Load("BC.TS.Application"),
    Assembly.Load("BC.TS.Domain")
    );

builder.Services.AddProblemDetails();

// Swagger document
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Task Scheduling",
        Version = "v1",
        Description = ".Net core web api for candidate & examiner task scheduling."
    });

    // Open the xml document
    var xmlName = $"{Assembly.GetExecutingAssembly().GetName().Name ?? nameof(BC.TS.Api)}.xml";
    var xmlPath = $"{Path.Combine(AppContext.BaseDirectory, xmlName)}";
    options.IncludeXmlComments(xmlPath, true);

    // Add JWT Authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = $"AppCode {builder.Configuration["AppSettings:Authentication:AppCode"]}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<AddRequiredHeaderParameterFilter>();
});

Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateBootstrapLogger();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.SpectreConsole("{Timestamp:HH:mm:ss} [{Level:u4}] {Message:lj}{NewLine}{Exception}",
        LogEventLevel.Error)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
    .Enrich.WithSpan()
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

// Scope service injection
builder.Services.AddScoped<IDispatcherApplication, DispatcherApplication>();
builder.Services.AddScoped<IDispatcherDomain, DispatcherDomain>();
builder.Services.AddScoped<IDispatcherFactory, DispatcherFactory>();
builder.Services.AddScoped<IDispatcherRepo, DispatcherRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseProblemDetails();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapHealthChecks("health", new HealthCheckOptions()
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});
app.Run();
