using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using test_api_dotnet.Database;
using test_api_dotnet.Models;
using test_api_dotnet.Models.DTOs;

namespace test_api_dotnet.Services
{
    public class PaymentService 
    {
        private readonly IRepository<Payment> _repository;
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<Payment> repository, MyDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaymentDto> AddAsync(CreatePaymentDto dto, string userId)
        {
            var payment = _mapper.Map<Payment>(dto);
            payment.CreatedAt = DateTime.UtcNow;
            payment.Status = PaymentStatus.Pending;
            payment.UserId = userId;
            await _repository.AddAsync(payment);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> GetAsync(int id)
        {
            var payment = await _repository.FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments = await _repository.ListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> UpdateAsync(int id, UpdatePaymentDto dto)
        {
            var payment = await _repository.FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            _mapper.Map(dto, payment);
            _repository.Update(payment);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> ChangeStatusAsync(int id, ChangePaymentStatusDto dto)
        {
            var payment = await _repository.FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            payment.Status = dto.Status;
            _repository.Update(payment);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task RemoveAsync(int id)
        {
            var payment = await _repository.FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            _repository.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
} 