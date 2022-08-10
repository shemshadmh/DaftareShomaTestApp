
using System.Data;
using System.Net.Mime;
using System.Reflection;
using Application;
using Application.Persons.Commands.CreatePerson;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Persistence;
using Serilog;
using TestApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});



builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePersonCommandValidator>());

builder.Services
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

#region swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v0", new OpenApiInfo
    {
        Version = "v0",
        Title = "Daftare Shoma Api",
        Contact = new OpenApiContact
        {
            Name = "MHShemshad"
        }
    });
    //Collect all referenced projects output XML document file paths  
    var currentAssembly = Assembly.GetExecutingAssembly();
    var xmlDocs = currentAssembly.GetReferencedAssemblies()
        .Union(new AssemblyName[] { currentAssembly.GetName() })
        .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location)!, $"{a.Name}.xml"))
        .Where(File.Exists).ToArray();
    Array.ForEach(xmlDocs, (d) =>
    {
        c.IncludeXmlComments(d);
    });
});
#endregion


var app = builder.Build();

using(var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

//Remove on release
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DefaultModelsExpandDepth(-1);
    options.SwaggerEndpoint("/swagger/v0/swagger.json", "Daftere Shoma API v0");
});

app.UseRouting();

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

#region Ex

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        await context.Response.WriteAsync("An exception was thrown from custom middleware!\r\n");

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if(exceptionHandlerPathFeature?.Error is DuplicateNameException)
        {
            await context.Response.WriteAsync("Duplicate Error Exception.");
        }
        else
        {
            await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error!.Message!);
        }
    });
});

#endregion

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller=persons}/{action=Index}/{id?}");
});
Log.Logger.Warning("Using {env} Configuration.", app.Environment.EnvironmentName);

app.Run();
