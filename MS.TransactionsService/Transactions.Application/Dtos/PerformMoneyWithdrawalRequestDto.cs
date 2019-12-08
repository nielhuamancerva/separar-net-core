using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Application.Dtos
{
    public class PerformMoneyWithdrawalRequestDto
    {
        public string DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
