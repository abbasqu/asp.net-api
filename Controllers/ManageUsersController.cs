
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_api_dotnet.Database;
using test_api_dotnet.Models;

namespace test_api_dotnet.Controllers;

[ApiController]
[Route("[controller]")]
public class ManageUsersController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    public ManageUsersController(MyDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    [HttpGet()]
    public async Task Get()
    {
        var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = _serviceProvider.GetRequiredService<UserManager<AppUser>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var user = await userManager.FindByEmailAsync("sareh@gmail.com");
        await userManager.AddToRoleAsync(user, "Admin");
    }
}