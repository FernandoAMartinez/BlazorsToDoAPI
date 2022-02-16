using BlazorsToDoAPI.Models;
using BlazorsToDoAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Services Builder

// Set the buildes for the DI container
builder.Services.AddEnpointDefinitnions(typeof(User));

//Add Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateActor = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", 
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
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