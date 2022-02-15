namespace BlazorsToDoAPI.Models;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public string JWT { get; set; }
}