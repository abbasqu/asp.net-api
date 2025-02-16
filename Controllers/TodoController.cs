using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_api_dotnet.Models;
using test_api_dotnet.Services;

namespace test_api_dotnet.Controllers;

[ApiController]
[Route("todo")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly TodoItemService _todoItemService;

    public TodoController(TodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }

    [HttpGet()]
    public async Task<IEnumerable<TodoItemGetDto>> Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //v1
        //var data = await _context.TodoItems.Where(c => c.UserId == userId).ToListAsync();
        //v2
        // var data = await _repoTodoItem.FindAsync(c => c.UserId == userId);

        // v1 & v2
        //return _mapper.Map<IEnumerable<TodoItemGetDto>>(data);

        // v3
        return await _todoItemService.GetAllAsync(userId);
    }

    [HttpPost()]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(TodoItemCreateDto item)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _todoItemService.AddAsync(item, userId);

        return Ok("Create");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItemUpdateDto item)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            await _todoItemService.UpdateAsync(item, id, userId);
        }
        catch
        {
            return NotFound();
        }

        return Ok("Update");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> ChangeStatus(int id, TodoItemChangeStatusDto item)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        try
        {
            await _todoItemService.ChangeStatusAsync(item, id, userId);
        }
        catch
        {
            return NotFound();
        }
        return Ok("ChangeStatus");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            await _todoItemService.RemoveAsync(id, userId);
        }
        catch
        {
            return NotFound();
        }

        return Ok("Delete");
    }
}

public class TodoItemCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

}

public class TodoItemUpdateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

}

public class TodoItemGetDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Description { get; set; }
}

public class TodoItemChangeStatusDto
{
    public bool IsComplete { get; set; }
}