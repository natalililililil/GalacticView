using Contracts;
using GalacticViewWebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using GalacticViewWebAPI.ActionFilters;
using Shared.DataTransferObjects;
using Service.DataShaping;
using GalacticViewWebAPI.Presentation.ActionFilters;
using GalacticViewWebAPI.Utility;
using AspNetCoreRateLimit;

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
services.ConfigureVersioning();
services.ConfigureResponseCaching();
services.ConfigureHttpCacheHeaders();
services.AddMemoryCache();
services.ConfigureRateLimitingOptions();
services.AddHttpContextAccessor();
services.AddAuthentication();
services.ConfigureIdentity();
services.ConfigureJWT(builder.Configuration);

services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
.Services.BuildServiceProvider()
.GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
.OfType<NewtonsoftJsonPatchInputFormatter>().First();

services.AddScoped<ValidationFilterAttribute>();
services.AddScoped<IDataShaper<SatelliteDto>, DataShaper<SatelliteDto>>();
services.AddScoped<ValidateMediaTypeAttribute>();
services.AddScoped<ISatelliteLinks, SatelliteLinks>();

services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile{ Duration = 120});
}).AddXmlDataContractSerializerFormatters()
.AddCustomCSVFormatter()
.AddApplicationPart(typeof(GalacticViewWebAPI.Presentation.AssemblyReference).Assembly);
services.AddCustomMediaTipe();

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

app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
