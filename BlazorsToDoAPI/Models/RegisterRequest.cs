using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorsToDoAPI.Models
{
    public class RegisterRequest
    {
        public RegisterRequest() { Id = new Guid(); }
        public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] [JsonIgnore] [Compare("Password")] public string PasswordValidator { get; set; }

    }
}
