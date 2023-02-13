using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.Repositories;
using Transaction.Api.Entities;
using Transaction.Api.Dtos;

namespace Transaction.Api.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController : Controller
{
    // private readonly ILogger<TransactionController> _logger;
    private readonly IRepository<Transactions> _repository;

    public TransactionController(IRepository<Transactions> repository)
    {
        _repository = repository;
        // _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<TransactionDto>> GetAll()
    {
        var items = (await _repository.GetAllAsync()).Select(item => item.AsDto());

        // var items = (await _repository.GetAllAsync()).ToList();
        return items;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetAsync(id);

        if (item == null)
            return NotFound();
        return item.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDto>> Create(CreateTransactionDto currentTransation)
    {
        var item = new Transactions
        {
            Id = Guid.NewGuid(),
            NameOfMerchant = currentTransation.NameOfMerchant,
            Price = currentTransation.Price,
            CreatedTime = DateTimeOffset.UtcNow
        };

        Console.WriteLine(item);

        await _repository.CreateAsync(item);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }
}

