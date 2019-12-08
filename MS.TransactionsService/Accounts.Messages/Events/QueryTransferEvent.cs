using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;
namespace Accounts.Messages.Events
{
    public class QueryTransferEvent:IEvent 
    {
        public string TransferId { get; private set; }
        public string SourceAccountId { get; private set; }
        public string DestinationAccountId { get; private set; }
        public decimal Amount { get; private set; }

        public QueryTransferEvent(string transferId, string sourceAccountId, string destinationAccountId, decimal amount)
        {
            TransferId = transferId;
            SourceAccountId = sourceAccountId;
            DestinationAccountId = destinationAccountId;
            Amount = amount;
        }
    }
}
