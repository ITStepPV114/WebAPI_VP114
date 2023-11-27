using Core.Interfaces;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Infrastructure.Data;
using Infrastructure;
using Core.Services;
using Core;
using WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

//get connection locacl SQL server string
//string connection = builder.Configuration.GetConnectionString("CinemaContextConnection") ?? throw new InvalidOperationException("Connection string 'ShopMVCConnection' not found.");

//get connection SQL Azure string
string connection = builder.Configuration.GetConnectionString("AzureConnection") ?? throw new InvalidOperationException("Connection string 'ShopMVCConnection' not found.");

builder.Services.AddDbContext(connection);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
    //option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddIdentity();
//added using extensions
builder.Services.AddRepository();
builder.Services.AddValidators();
builder.Services.AddAutoMapper();

builder.Services.AddCustomServices();

//add  jwt-token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer =  builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

    };
});

//add CORS
builder.Services.AddCors(options =>
{
options.AddPolicy(name: MyAllowSpecificOrigins,
                  builder =>
                  {
                      //builder.WithOrigins("http://localhost:3000");
                      builder.AllowAnyHeader();
                      builder.AllowAnyMethod();
                      builder.AllowAnyOrigin();
                      //builder.AllowCredentials();

                  });

});
//builder.Services.AddScoped<IMoviesService, MoviesService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//global hadler middleware
app.UseMiddleware<ErrorHandlerMiddleware>();
//use Cors
app.UseCors(MyAllowSpecificOrigins);

//app.UseCors(opt =>
//{
//    opt.AllowAnyHeader();
//    opt.AllowAnyMethod();
//    opt.AllowCredentials();
//    opt.AllowAnyOrigin();
//});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
