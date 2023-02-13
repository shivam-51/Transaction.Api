using System;
using Transaction.Api.Entities;

namespace Transaction.Api.Dtos
{
    public record TransactionDto(Guid Id, string? NameOfMerchant, decimal Price, DateTime CreatedTime, Guid storeId, Status Status = Status.Pending);
    
    public record CreateTransactionDto(string NameOfMerchant, decimal Price, Guid StoreId, Status Status = Status.Pending);
}