using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_api_dotnet.Models;

namespace test_api_dotnet.Controllers;

[ApiController]
[Route("todo")]
public class TodoController : ControllerBase
{
    private readonly MyDbContext _context;
    public TodoController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<IEnumerable<TodoItem>> Get()
    {
        return await _context.TodoItems.ToListAsync();
    }

    [HttpPost()]
    public async Task<IActionResult> Create(TodoItemCreateDto item)
    {
        var newTodo = new TodoItem
        {
            Name = item.Name,
            IsComplete = false
        };

        _context.TodoItems.Add(newTodo);
        await _context.SaveChangesAsync();
        return Ok("Create");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItemUpdateDto item)
    {
        var todoItem = await _context.TodoItems.FirstOrDefaultAsync(c => c.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        todoItem.Name = item.Name;
        await _context.SaveChangesAsync();
        return Ok("Update");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> ChangeStatus(int id, TodoItemChangeStatusDto item)
    {
        var todoItem = await _context.TodoItems.FirstOrDefaultAsync(c => c.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        todoItem.IsComplete = item.IsComplete;
        await _context.SaveChangesAsync();
        return Ok("ChangeStatus");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var todoItem = await _context.TodoItems.FirstOrDefaultAsync(c => c.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return Ok("Delete");
    }
}

public class TodoItemCreateDto
{
    public string Name { get; set; } = null!;
}

public class TodoItemUpdateDto
{
    public string Name { get; set; } = null!;
}

public class TodoItemChangeStatusDto
{
    public bool IsComplete { get; set; }
}