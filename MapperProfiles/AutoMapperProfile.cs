using AutoMapper;
using test_api_dotnet.Controllers;
using test_api_dotnet.Models;

namespace test_api_dotnet.MapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemCreateDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemUpdateDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemChangeStatusDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemGetDto>().ReverseMap();
    }
}