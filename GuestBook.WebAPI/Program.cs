using FluentValidation;
using GuestBook.BAL;
using GuestBook.BAL.BL;
using GuestBook.BAL.DTO;
using GuestBook.BAL.Interfaces;
using GuestBook.BAL.Services;
using GuestBook.BAL.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Inject Validators
builder.Services.AddInfraStructure();
builder.Services.AddScoped<IAuthBL, AuthBL>();
builder.Services.AddScoped<IMessageBL, MessageBL>();
builder.Services.AddScoped<IValidator<AuthRequestDTO>, AuthValidator>();
builder.Services.AddScoped<IValidator<AddMessageDTO>, AddMessageValidator>();
builder.Services.AddScoped<IValidator<UpdateMessageDTO>, UpdateMessageValidator>();
builder.Services.AddScoped<ITokenServiceProvider, TokenServiceProvider>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.ConfigureJWTAuth(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
