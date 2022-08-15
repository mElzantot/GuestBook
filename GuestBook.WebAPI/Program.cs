using FluentValidation;
using GuestBook.BAL;
using GuestBook.BAL.BL;
using GuestBook.BAL.DTO;
using GuestBook.BAL.Interfaces;
using GuestBook.BAL.Services;
using GuestBook.BAL.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "", Version = "" });

    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                        },
                        Array.Empty<string>()
                    }
                });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
