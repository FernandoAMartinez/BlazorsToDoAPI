using MongoDB.Bson;
using MongoDB.Driver;
using BlazorsToDoAPI.Extensions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace BlazorsToDoAPI.EndpointDefinitions
{
    public class MongoEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app) { }

        public void DefineServices(IServiceCollection services)
        {

            //Build specific services for BSON Serialization
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.DateTime));

            services.AddSingleton<IMongoClient>(options =>
            {
                var login = Environment.GetEnvironmentVariable("MONGODB_USER"); 
                var pass = Environment.GetEnvironmentVariable("MONGODB_PASS");
                var server = Environment.GetEnvironmentVariable("MONGODB_SERVER");
                var database = Environment.GetEnvironmentVariable("MONGODB_BASENAME");

                return new MongoClient(String.Format($"mongodb+srv://{login}:{pass}@{server}/{database}?retryWrites=true&w=majority"));
            });

            services.AddScoped(options => options.GetService<IMongoClient>().StartSession());
        }
    }
}
