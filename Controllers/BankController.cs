using Microsoft.AspNetCore.Mvc;
using test_api_dotnet.Models.DTOs;
using test_api_dotnet.Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly BankService _bankService;

        public BankController(BankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDto>>> GetAll()
        {
            var banks = await _bankService.GetAllAsync();
            return Ok(banks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankDto>> GetById(int id)
        {
            var bank = await _bankService.GetByIdAsync(id);
            return Ok(bank);
        }

        [HttpPost]
        public async Task<ActionResult<BankDto>> Create(CreateBankDto createBankDto)
        {
            var bank = await _bankService.CreateAsync(createBankDto);
            return CreatedAtAction(nameof(GetById), new { id = bank.Id }, bank);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BankDto>> Update(int id, UpdateBankDto updateBankDto)
        {
            var bank = await _bankService.UpdateAsync(id, updateBankDto);
            return Ok(bank);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bankService.DeleteAsync(id);
            return NoContent();
        }
    }
} 