using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BlazorsToDoAPI.Models
{
    public class User
    {
        public User() { Id = new Guid(); }
        [JsonPropertyName("_id")] public Guid Id { get; set; }
        [JsonPropertyName("email")] public string Email { get; set; }
        [JsonIgnore][JsonPropertyName("password")] public string Password { get; set; }
        [JsonPropertyName("lastJWT")] public string LastJWT { get; set; }
    }

    public class RegisterUser
    {
        public RegisterUser() { Id = new Guid(); }
        [JsonIgnore][JsonPropertyName("_id")] public Guid Id { get; set; }
        [JsonPropertyName("email")] public string Email { get; set; }
        [JsonPropertyName("password")] public string Password { get; set; }
        [JsonPropertyName("lastJWT")] public string LastJWT { get; set; }
    }
}
