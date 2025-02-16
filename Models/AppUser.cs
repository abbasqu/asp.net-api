using Microsoft.AspNetCore.Identity;

namespace test_api_dotnet.Models;

public class AppUser : IdentityUser
{
    public string? ProfilePicture { get; set; }

    public ICollection<TodoItem>? TodoItems { get; set; }
    public ICollection<Payment>? Payments { get; set; }
}
