using BlazorsToDoAPI.Extensions;
using BlazorsToDoAPI.Helpers;
using BlazorsToDoAPI.Models;
using BlazorsToDoAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorsToDoAPI.EndpointDefinitions;
public class LoginEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("auth/register", async ([FromBody] RegisterRequest registerUser, ILoginRepository repository) =>
            await repository.Create(new User()
            {
                Id = registerUser.Id,
                Name = registerUser.Name,
                Email = registerUser.Email,
                Password = registerUser.Password
            })).AllowAnonymous()
            .Produces<User>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Register User")
            .WithTags("User Management");
             
        app.MapPost("auth/login", async ([FromBody] LoginRequest request, ILoginRepository loginRepository) =>
        {
            var jwtHelper = new JWTHelper(Environment.GetEnvironmentVariable("JWT_SECRET"));

            try
            {
                var logedUser = await loginRepository.GetLoginFromCredentialsAsync(request);
                    
                if(logedUser is not null)
                {
                    var token = jwtHelper.CreateToken(request.Email);

                    if (token is not null)
                    {
                        //logedUser.LastJWT = token;
                        //return new
                        //{
                        //    UserId = logedUser.Id,
                        //    //logedUser.Name,
                        //    //JWT = logedUser.LastJWT
                        //    JWT = token
                        //};
                        return new LoginResponse()
                        {
                            UserId = logedUser.Id,
                            JWT = token
                        };
                    }
                    else { return null; }
                }
                else { return null; }

            }
            catch (Exception ex) { return null; }
        }).AllowAnonymous()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithName("Login User")
        .WithTags("User Management");
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<ILoginRepository, LoginRepository>();
    }
}