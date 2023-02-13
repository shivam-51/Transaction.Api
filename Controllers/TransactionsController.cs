using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.Repositories;
using Transaction.Api.Entities;
using Transaction.Api.Dtos;

namespace Transaction.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : Controller
{
    private readonly IRepository<Transactions> _repository;

    public TransactionController(IRepository<Transactions> repository)
    {
        _repository = repository;
    }

    // [HttpGet]
    // public async Task<IEnumerable<TransactionDto>> GetAll([FromQuery] PagingParams pagingParms)
    // {
    //     var items = (await _repository.GetAllAsync(pagingParms)).Select(item => item.AsDto());
    //     return items;
    // }

    [HttpGet]
    public async Task<IEnumerable<TransactionDto>> GetWithFilters([FromQuery] PagingParams pagingParams, [FromQuery] QueryParams queryParams)
    {
        var items = (await _repository.GetWithFilters(pagingParams, queryParams)).Select(item => item.AsDto());

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
            StoreId = currentTransation.StoreId,
            Status = currentTransation.Status,
            CreatedTime = DateTime.Now
        };
        await _repository.CreateAsync(item);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }
}

