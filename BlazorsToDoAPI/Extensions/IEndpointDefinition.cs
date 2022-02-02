﻿namespace BlazorsToDoAPI.Extensions
{
    public interface IEndpointDefinition
    {
        void DefineEndpoints(WebApplication app);
        void DefineServices(IServiceCollection services);
    }
}
