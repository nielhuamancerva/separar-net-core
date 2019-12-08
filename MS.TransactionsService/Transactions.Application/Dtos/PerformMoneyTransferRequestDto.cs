using System;

namespace Transactions.Application.Dtos
{
    public class PerformMoneyTransferRequestDto
    {
        public string SourceAccountId { get; set; }
        public string DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}