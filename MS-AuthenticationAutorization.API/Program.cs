using System.Reflection;
using System.Text.Json.Serialization;
using Asp.Versioning;
using CroosCutting.MS_AuthenticationAutorization.IoC;
using CroosCutting.MS_AuthenticationAutorization.Middleware;
using Microsoft.OpenApi.Models;
using CrossCutting.MS_AuthenticationAutorization.IoC;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    });
//Versionamento
builder.Services.AddApiVersioning(v =>
{
    v.DefaultApiVersion = new ApiVersion(1.0);
    v.AssumeDefaultVersionWhenUnspecified = true;
    v.ReportApiVersions = true;
    v.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new QueryStringApiVersionReader()
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.InjectDataBase(builder.Configuration);
builder.Services.InjectDependency();
builder.Services.AddJwtAuthentication(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MS-AuthenticatonAutorization",
        Description = "Autentica��o e autoriza��o de us��rios"
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddCorsPolicy(builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
