using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using test_api_dotnet.Database;
using test_api_dotnet.Models;
using test_api_dotnet.Models.DTOs;

namespace test_api_dotnet.Services
{
    public class BankService 
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public BankService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankDto>> GetAllAsync()
        {
            var banks = await _context.Banks.ToListAsync();
            return _mapper.Map<IEnumerable<BankDto>>(banks);
        }

        public async Task<BankDto> GetByIdAsync(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
                throw new KeyNotFoundException("Bank not found");

            return _mapper.Map<BankDto>(bank);
        }

        public async Task<BankDto> CreateAsync(CreateBankDto createBankDto)
        {
            var bank = _mapper.Map<Bank>(createBankDto);
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();

            return _mapper.Map<BankDto>(bank);
        }

        public async Task<BankDto> UpdateAsync(int id, UpdateBankDto updateBankDto)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
                throw new KeyNotFoundException("Bank not found");

            _mapper.Map(updateBankDto, bank);
            await _context.SaveChangesAsync();

            return _mapper.Map<BankDto>(bank);
        }

        public async Task DeleteAsync(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
                throw new KeyNotFoundException("Bank not found");

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();
        }
    }
} 