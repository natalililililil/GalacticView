using AutoMapper;
using Contracts;
using GalacticViewWebAPI.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddCors();
services.AddAuthorization();

services.ConfigureCors();
services.ConfigureIISIntegration();
services.ConfigureLoggerService();
services.ConfigureSqlContext(builder.Configuration);
services.ConfigureRepositoryManager();
services.ConfigureServiceManager();
services.AddAutoMapper(typeof(Program));

services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
.AddCustomCSVFormatter()
.AddApplicationPart(typeof(GalacticViewWebAPI.Presentation.AssemblyReference).Assembly);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var app = builder.Build();


var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
