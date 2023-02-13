using System;
namespace Transaction.Api.Entities
{
    public class Transactions: IEntity
    {
        public Guid Id { get; set; }

        public string? NameOfMerchant { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedTime { get; set; }
    }
}

