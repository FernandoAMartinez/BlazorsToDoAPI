using BlazorsToDoAPI.Models;
using BlazorsToDoAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
#region Services Builder

// Set the buildes for the DI container
builder.Services.AddEnpointDefinitnions(typeof(User));

//Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader());
});

#endregion
var app = builder.Build();
#region App Configuration

app.UseCors("Open");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseEndpointDefinitions();
#endregion

app.Run();