using AutoMapper;
using test_api_dotnet.Controllers;
using test_api_dotnet.Models;
using test_api_dotnet.Models.DTOs;

namespace test_api_dotnet.MapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemCreateDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemUpdateDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemChangeStatusDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemGetDto>().ReverseMap();

        CreateMap<Payment, CreatePaymentDto>().ReverseMap();
        CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
        CreateMap<Payment, ChangePaymentStatusDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();


        CreateMap<Bank, BankDto>();
        CreateMap<CreateBankDto, Bank>();
        CreateMap<UpdateBankDto, Bank>();
    }
}