using System;

namespace Transaction.Api.Dtos
{
    public record TransactionDto(Guid Id, string? NameOfMerchant, decimal Price, DateTimeOffset CreatedDate);
    
    public record CreateTransactionDto(string NameOfMerchant, decimal Price);
}