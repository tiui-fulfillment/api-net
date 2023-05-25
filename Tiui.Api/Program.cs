using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiui.Api.Helpers;
using Tiui.Application.Filters;
using Tiui.Application.Mapper;
using Tiui.Application.Services.websocket;
using Tiui.Data;
using Tiui.Security;
using Tiui.Services.GuiaNotificationClientes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

#region Log
var path = Directory.GetCurrentDirectory();
var log = new LoggerConfiguration()
    .WriteTo.File($@"{path}\Logs\Log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.ConfigureLogging(loggin =>
{
    loggin.AddSerilog(log);
});
#endregion

#region Services
builder.Services.AddSingleton<Encoding>(Encoding.UTF8);
builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(AppExceptionHandler));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TiuiDBContext>(options =>
             options.UseNpgsql("Host=tiui-prod.cluster-cp0tdihlsymi.us-east-1.rds.amazonaws.com;Database=TiuiDB-dev;Username=postgres;Password=Asdf1234$;"));
builder.Services.AddSingleton<NpgsqlConnection>(options =>
new NpgsqlConnection("Host=tiui-prod.cluster-cp0tdihlsymi.us-east-1.rds.amazonaws.com;Database=TiuiDB-dev;Username=postgres;Password=Asdf1234$;"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDependency();
builder.Services.AddAutoMapper(typeof(AutoMapping));
builder.Services.AddSingleton(builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>());
builder.Services.AddSingleton<WebSocketConnectionManager>();
builder.Services.AddSingleton<GuiaNotificationClientes>();

#endregion

#region JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["KEY_JWT_TIUI"])),
        ClockSkew = TimeSpan.Zero
    });
#endregion

#region Cors
var MyAllowSpecificOrigins = "_tiuiorigensapp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});
#endregion

#region App
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
var serviceProvider = app.Services;
var guiaNotificationClientes = serviceProvider.GetRequiredService<GuiaNotificationClientes>();

// Obtén el token de cancelación del proveedor de la aplicación
var cancellationToken = app.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping;

await guiaNotificationClientes.StartAsync(cancellationToken);

// Configure the HTTP request pipeline.
/* if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});*/

app.UseCors(MyAllowSpecificOrigins);

//app.UseHttpsRedirection();

app.UseWebSockets();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion