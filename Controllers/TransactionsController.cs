using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.Repositories;
using Transaction.Api.Entities;
using Transaction.Api.Dtos;
using Dapr.Client;

namespace Transaction.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : Controller
{
    private readonly IRepository<Transactions> _repository;
    private readonly DaprClient _daprClient;

    public TransactionController(IRepository<Transactions> repository, DaprClient daprClient)
    {
        _repository = repository;
        _daprClient = daprClient;
    }

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
        
        string PUBSUB_NAME = "servicebusdapr";
        string TOPIC_NAME = "daprtopic";
        var transaction = new Transactions();
        await _daprClient.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, "transaction");
        Console.WriteLine("Published data: " + transaction.Id);
        
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

