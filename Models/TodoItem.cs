
using Microsoft.AspNetCore.Identity;

namespace test_api_dotnet.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
    public AppUser? User { get; set; }
    public string? UserId { get; set; }
}