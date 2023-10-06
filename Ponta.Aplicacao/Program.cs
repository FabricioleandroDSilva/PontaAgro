using Ponta.Aplicacao.ServicesCollections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ponta.Infra.Contexto;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Serilog;

var AllowSpecificOrigins = "_allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PostgrSqlContext>(options =>
{
options.UseNpgsql(builder.Configuration["ConnectionString"], opt =>
{
    opt.CommandTimeout(180);
    opt.EnableRetryOnFailure(5);
});
});

builder.Services.AddHttpContextAccessor();
builder.Services.AdicionarServicos().AdicionarModelos();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddCors(options =>
{
options.AddPolicy(name: AllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
});

builder.Services.AdicionarServicos().AdicionarModelos();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddCors(options =>
{
options.AddPolicy(name: AllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
});

builder.Services.AddAuthentication(x =>
{
x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


})

            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo { Title = "PontaAgro App", Version = string.Concat("V: ", builder.Configuration["Version"]), });
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT Autorização header esquema Bearer.\r\n\r\n 1. Login, 2. Copie o token conforme o exemplo. \r\n\r\nExemplo: \"Bearer 12345abcdef\"",
});
options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                        }
                    });

});

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(options =>
{
  
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "PontaAgro - Catalogo de Endpoints", Version = string.Concat("V:", builder.Configuration["Version"]) });
});

builder.Services.AddControllersWithViews()
.AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Logging.ClearProviders();

builder.Logging.ClearProviders();

var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["Observabilidade"], rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .Enrich.With()
    .CreateLogger();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PostgrSqlContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}


app.UseCors(AllowSpecificOrigins);
app.UseHsts();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
options.RoutePrefix = string.Empty;

});

app.UseDeveloperExceptionPage();

app.UseSwagger(options =>
{
options.SerializeAsV2 = true;
});


app.UseStaticFiles();
app.UseHttpLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{

endpoints.MapControllers();
});
app.Run();
