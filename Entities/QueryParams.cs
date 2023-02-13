namespace Transaction.Api.Entities
{
    public class QueryParams
    {
        public Guid StoreId { get; set; }
        public string NameOfMerchant { get; set; } = "";
        public Status Status { get; set; } = Status.Other;
        public decimal PriceFrom { get; set; } = Decimal.MinValue;
        public decimal PriceTo { get; set; } = Decimal.MaxValue;
        public DateTime? FromDate { get; set; } = DateTime.MinValue;
        public DateTime? ToDate { get; set; } = DateTime.MaxValue;
    }
}

