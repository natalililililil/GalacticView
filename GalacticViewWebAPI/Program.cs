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

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
});
app.UseAuthorization();

//link: https://localhost:7128/weatherforecast
app.MapGet("/weatherforecast", ((ILoggerManager logger) =>
{
    ILoggerManager _logger = logger;
    _logger.LogInfo("Here is info message from our values controller.");
    _logger.LogDebug("Here is debug message from our values controller.");
    _logger.LogWarn("Here is warn message from our values controller.");
    _logger.LogError("Here is an error message from our values controller.");
    return new string[] { "value1", "value2" };
}));

app.Run();
