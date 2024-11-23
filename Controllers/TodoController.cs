using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_api_dotnet.Models;

namespace test_api_dotnet.Controllers;

[ApiController]
[Route("todo")]
public class TodoController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;

    public TodoController(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet()]
    public async Task<IEnumerable<TodoItemGetDto>> Get()
    {
        var data = await _context.TodoItems.ToListAsync();
        return _mapper.Map<IEnumerable<TodoItemGetDto>>(data);
    }

    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> Create(TodoItemCreateDto item)
    {
        var newTodo = _mapper.Map<TodoItem>(item);

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

        _mapper.Map(item, todoItem);

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

        _mapper.Map(item, todoItem);

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