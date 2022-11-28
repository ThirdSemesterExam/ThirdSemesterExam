using System.Text;
using Application;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("initializing");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

#region AutoMapper
var mapper = new MapperConfiguration(configuration =>
{
    configuration.CreateMap<PostPetsDTO, Pets>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Database

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(
    "Data source=db.db"
    ));
#endregion

#region Dependency Resolver Service

 builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));  
builder.Services.AddScoped<IPetsService, PetsService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPetsRepository, PetsRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();  
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>    
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
#endregion

#region policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", (policy) => { policy.RequireRole("Admin");});  
});
#endregion
builder.Services.AddCors();

var app = builder.Build();

#region Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
#endregion

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthentication();      
app.UseAuthorization();      

app.MapControllers();

app.Run();
