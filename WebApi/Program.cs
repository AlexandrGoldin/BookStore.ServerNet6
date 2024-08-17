using Application;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.EfData;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using WebApi;
using WebApi.Common.WebApiConfigureServices;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var connectString = builder.Configuration.GetConnectionString("BookStoreDb");
builder.Services.AddDbContext<EfBookStoreContext>(opt => opt.UseSqlServer(connectString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddWebApiServices();

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddVersionedApiExplorer(options =>
                options.GroupNameFormat = "'v'VVV");

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
                    ConfigureSwaggerOptions>();

builder.Services.AddApiVersioning();

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//    .WriteTo.Console()
//    .WriteTo.File(new JsonFormatter(), @"Logs\log-.txt", rollingInterval:
//        RollingInterval.Minute)
//    .CreateLogger();
//builder.Host.UseSerilog(Log.Logger);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwaggerUI(config =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        config.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
        config.RoutePrefix = string.Empty;
    }
});

app.UseStaticFiles();

app.UseCustomExceptionHandler();

app.UseRouting();
app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();

app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod());

app.UseApiVersioning();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

public partial class Program { }
