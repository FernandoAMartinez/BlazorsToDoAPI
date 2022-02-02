using BlazorsToDoAPI.Extensions;
using BlazorsToDoAPI.Helpers;
using BlazorsToDoAPI.Models;
using BlazorsToDoAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorsToDoAPI.EndpointDefinitions
{
    public class LoginEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/users",
                [Authorize]
                async (ILoginRepository repository) =>
                    await repository.GetAll())
                .Produces<IEnumerable<User>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("Get List of Users")
                .WithTags("User Management")
                .RequireAuthorization();

            app.MapPost("/register", 
                async ([FromBody] RegisterUser registerUser,
                ILoginRepository repository) =>
                await repository.Create(new User()
                {
                    Id = registerUser.Id,
                    Email = registerUser.Email,
                    Password = registerUser.Password,
                    LastJWT = registerUser.LastJWT
                })).AllowAnonymous()
                .Produces<User>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("Register User")
                .WithTags("User Management");

            app.MapPost("/login",
            async ([FromBody] LoginRequest request,
            ILoginRepository loginRepository) =>
            {
                var jwtHelper = new JWTHelper(Environment.GetEnvironmentVariable("JWT_SECRET"));
                try
                {
                    var logedUser = await loginRepository.GetLoginFromCredentialsAsync(request);
                    if(logedUser is not null)
                    {
                        var token = jwtHelper.CreateToken(request.Email);

                        if(token is not null)
                        {
                            logedUser.LastJWT = token;
                            return logedUser.LastJWT;
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
}
