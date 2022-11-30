using System.Net.Mime;
using System.Reflection;
using System.Text;
using Application;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("initializing");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());


var mapper = new MapperConfiguration(configuration =>
{
    configuration.CreateMap<PostPetsDTO, Pets>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(
    "Data source=db.db"
    ));



//dependency resolver service. 
//this AddScoped provides f-example this IProductService capable of being called through a dependency injection for other components inside of our system. 
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings")); // comes from appsetings.json file.
builder.Services.AddScoped<IPetsService, PetsService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPetsRepository, PetsRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>(); // this AuthenticationService is our own class.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>  // this AddAuthentication we use for securing the endpoints. this is something use to limit the user from the making requests that they are not authenticated for. Microsoft.ApsNetCore.Authentication.JwtBearer id NuGet pacakge and install it inside of API.
{
    options.TokenValidationParameters = new TokenValidationParameters  // option check the token.
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetValue<string>("AppSettings:Secret")))
    };
});

// we make some policy, that user can read a product and Admin can add and delete product.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", (policy) => { policy.RequireRole("Admin");});  // if the value at the role inside of is now Admin. this user will be authorized.
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthentication();     // enable UseAuthentication
app.UseAuthorization();     // enable UseAuthorization

app.MapControllers();

app.Run();
