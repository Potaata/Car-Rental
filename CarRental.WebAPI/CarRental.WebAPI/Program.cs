using CarRental.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using CarRental.Infrastructure.DI;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using CarRental.Infrastructure.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>();
        context.Response.ContentType = "application/json";
        try
        {
            ApiException apiExc = (ApiException)ex.Error;
            context.Response.StatusCode = (int)apiExc.StatusCode;
            string err = "{";
            err += $"\"errorMessage\":\"{apiExc.Message}\"";
            err += "}";
            await context.Response.WriteAsync(err).ConfigureAwait(false);
        }
        catch (InvalidCastException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string err = "{\"errorMessage\":\"The server could not process your request!\"}";
            await context.Response.WriteAsync(err).ConfigureAwait(false);
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
