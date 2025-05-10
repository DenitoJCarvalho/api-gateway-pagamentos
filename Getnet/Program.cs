using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;

using Getnet.Infrastrucutre.Configurations.Getnet;
using Getnet.Services.Interfaces;
using Getnet.Services;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Dependências
#region Dependências
builder.Services.AddScoped<IGetnetService, GetnetService>();
#endregion
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Configuração do CORS
#region CORS

var config = builder.Configuration.GetSection("Cors");
var origins = config["Origins"]?.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? new[] { "*" };
var methods = config["Methods"] ?? "GET, POST";
var headers = config["Headers"] ?? "Content-Type, Authorization";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCustomer", policy =>
    {
        if (origins.Length == 1 && origins[0] == "*")
        {
            policy.AllowAnyOrigin();
        }
        else
        {
            policy.WithOrigins(origins);
        }

        policy
            .WithMethods(methods.Split(","))
            .WithHeaders(headers.Split(","));
    });
});
#endregion

//Configuração do Getnet
#region Getnet
builder.Services.Configure<GetnetSettings>(
    builder.Configuration.GetSection("Getnet")
);

var getnetSettings = builder.Configuration.GetSection("GetNet").Get<GetnetSettings>();

//Configuração do Httpclient para o GetnetService
builder.Services.AddHttpClient<IGetnetService, GetnetService>((serviceProvider, client) =>
{
    var config = serviceProvider.GetRequiredService<IOptions<GetnetSettings>>().Value;
    var environment = serviceProvider.GetRequiredService<IHostEnvironment>();

    var host = environment.IsDevelopment()
        ? config.HostHomologacao
        : config.HostProducao;

    client.BaseAddress = new Uri($"https://{host}");
});

#endregion

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });


//Configuração do Swagger
#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Getnet API",
        Version = "v1",
        Description = "API de processos de pagamento para as plataformas da Pesados Online"
    });

    //Define o esquema OAuth2
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = @"Insira o token JWT no campo abaixo. Exemplo Bearer abc123",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });    

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Getnet API V1");
    });
}

app.UseCors("AllowCustomer");
app.UseHttpsRedirection();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.MapControllers();

app.Run();
