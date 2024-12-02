using Backend.Data;
using Backend.Extensions;
using Backend.Helper;
using Backend.Interfaces;
using Backend.Repo;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
var secretKey = config.GetSection("AppSettings:Key").Value;
var key = new SymmetricSecurityKey(Encoding.UTF8
    .GetBytes(secretKey));
builder.Services.AddDbContext<DataContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs"), sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Configuration.AddEnvironmentVariables("RENTIFY_");
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                // services.AddAuthentication("Bearer")
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key
                    };
                });
var app = builder.Build();
var env = builder.Environment;
// Configure the HTTP request pipeline.
app.ConfigureCustomExceptionHandler(env);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(m => m.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
