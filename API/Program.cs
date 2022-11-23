using Application;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(
    "Data source=db.db"
));
//dependency resolver service. 
//this AddScoped provides f-example this IProductService capable of being called through a dependency injection for other components inside of our system. 
builder.Services.AddScoped<IPetsService, PetsService>();
var mapper = new MapperConfiguration(configuration =>
{
    configuration.CreateMap<PostPetsDTO, Pets>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);


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