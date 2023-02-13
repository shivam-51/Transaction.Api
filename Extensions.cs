using System;
using Transaction.Api.Dtos;
using Transaction.Api.Entities;

namespace Transaction.Api
{
	public static class Extensions
	{
		public static TransactionDto AsDto(this Transactions ItemDto)
		{
			return new TransactionDto(ItemDto.Id, ItemDto.NameOfMerchant, ItemDto.Price, ItemDto.CreatedTime);
		}
	}
}

