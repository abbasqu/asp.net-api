using AutoMapper;
using test_api_dotnet.Controllers;
using test_api_dotnet.Database;
using test_api_dotnet.Models;

namespace test_api_dotnet.Services;

public interface ITodoItemService
{
    Task<IEnumerable<TodoItemGetDto>> GetAllAsync(string? userId);

    Task<TodoItem?> GetAsync(string? userId, int itemId);

    Task AddAsync(TodoItemCreateDto item, string? userId);

    Task UpdateAsync(TodoItemUpdateDto itemDto, int itemId, string? userId);

    Task ChangeStatusAsync(TodoItemChangeStatusDto itemDto, int itemId, string? userId);

    Task RemoveAsync(int itemId, string? userId);
}

public class TodoItemService : ITodoItemService
{
    private readonly IRepository<TodoItem> _todoItemRepository;
    private readonly IMapper _mapper;
    private readonly MyDbContext _context;

    public TodoItemService(MyDbContext context, IRepository<TodoItem> todoItemRepository, IMapper mapper)
    {
        _context = context;
        _todoItemRepository = todoItemRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(TodoItemCreateDto item, string? userId)
    {
        var newTodo = _mapper.Map<TodoItem>(item);

        newTodo.UserId = userId;

        await _todoItemRepository.AddAsync(newTodo);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TodoItemGetDto>> GetAllAsync(string? userId)
    {
        var data = await _todoItemRepository.ListAsync(c => c.UserId == userId);
        return _mapper.Map<IEnumerable<TodoItemGetDto>>(data);
    }

    public async Task<TodoItem?> GetAsync(string? userId, int itemId)
    {
        var item = await _todoItemRepository.FirstOrDefaultAsync(c => c.Id == itemId && c.UserId == userId);

        if (item == null)
        {
            return null;
        }

        return item;
    }

    public async Task UpdateAsync(TodoItemUpdateDto itemDto, int itemId, string? userId)
    {
        var item = await _todoItemRepository.FirstOrDefaultAsync(c => c.Id == itemId && c.UserId == userId);

        if (item == null)
        {
            throw new Exception("The Item is not exist!");
        }

        _mapper.Map(itemDto, item);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(TodoItemChangeStatusDto itemDto, int itemId, string? userId)
    {
        var item = await _todoItemRepository.FirstOrDefaultAsync(c => c.Id == itemId && c.UserId == userId);

        if (item == null)
        {
            throw new Exception("The Item is not exist!");
        }

        _mapper.Map(itemDto, item);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int itemId, string? userId)
    {
        var item = await _todoItemRepository.FirstOrDefaultAsync(c => c.Id == itemId && c.UserId == userId);

        if (item == null)
        {
            throw new Exception("The Item is not exist!");
        }
        _todoItemRepository.Remove(item);
        await _context.SaveChangesAsync();
    }
}

