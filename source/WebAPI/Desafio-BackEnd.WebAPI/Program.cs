using Desafio_BackEnd.Domain.Core.Data;
using Desafio_BackEnd.Infra.CrossCutting;
using Desafio_BackEnd.WebAPI.Configurations;
using Desafio_BackEnd.WebAPI.Configurations.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Injecao de Dependecia
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddSingleton(settings);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddServices();
#endregion

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null; //Forca PascalCase
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerSetup(settings.ApplicationName);

builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });

builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddAuthorization();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseResponseCompression();
app.UsePathBase($"/{settings.ApplicationBasePath}");
app.UseSwaggerSetup();

if (builder.Configuration.GetValue("LogEnvVariables", false))
    Console.WriteLine(builder.Configuration.GetDebugView());

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();